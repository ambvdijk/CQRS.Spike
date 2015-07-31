using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CQRS.Spike.Core;
using CQRS.Spike.Core.Serialization;

namespace CQRS.Spike.Infra.EntityFramework
{
  public class EventStore : IEventStore, IDisposable
  {
    private readonly AggregateContext _context;
    private readonly ITextSerializer _serializer;

    public EventStore(ITextSerializer serializer, string nameOrConnectionString)
    {
      _serializer = serializer;
      _context = new AggregateContext(nameOrConnectionString);
    }

    public void SaveEvents(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion)
    {
      using (var transaction = _context.Database.BeginTransaction())
      {
        try
        {
          var serializedEvents = events.ToArray();

          var aggregate = _context.Aggregates.Find(aggregateId);

          if (aggregate != null)
          {
            if (aggregate.Version != expectedVersion)
            {
              throw new DBConcurrencyException();
            }

            aggregate.Version += serializedEvents.Length;
          }
          else if (expectedVersion != 0)
          {
            throw new InvalidOperationException("Unable to apply events on non-existing aggregate!");
          }
          else
          {
            aggregate = new Aggregate
            {
              Id = aggregateId,
              Version = 1
            };

            _context.Aggregates.Add(aggregate);
          }

          _context.Events
            .AddRange
            (
              serializedEvents.Select
                (
                  (e, i) => new Event
                  {
                    Id = e.Id,
                    Version = expectedVersion + 1,
                    Data = Serialize(e)
                  }
                )
            );

          _context.SaveChanges();

          transaction.Commit();
        }
        catch (Exception)
        {
          transaction.Rollback();
          throw;
        }
      }
    }

    public IEnumerable<IEvent> GetEventsForAggregate(Guid aggregateId)
    {
      return _context.Events
        .Where(e => e.Id == aggregateId)
        .Select(e => e.Data)
        .AsEnumerable() //Change boundry to code-execution from here
        .Select(Deserialize);
    }

    private string Serialize(IEvent @event)
    {
      using (var writer = new StringWriter())
      {
        _serializer.Serialize(writer, @event);
        return writer.ToString();
      }
    }

    private IEvent Deserialize(string graph)
    {
      using (var reader = new StringReader(graph))
      {
        return (IEvent)_serializer.Deserialize(reader);
      }
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }

}

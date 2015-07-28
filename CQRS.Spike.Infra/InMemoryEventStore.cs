using System;
using System.Collections.Generic;
using System.Linq;
using CQRS.Spike.Core;

namespace CQRS.Spike.Infra
{
  public class InMemoryEventStore : IEventStore
  {
    private readonly IDictionary<Guid, ICollection<IEvent>> _events;

    public InMemoryEventStore()
    {
      _events = new Dictionary<Guid, ICollection<IEvent>>();
    }

    public void SaveEvents(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion)
    {
      ICollection<IEvent> eventCollection;
      if (!_events.TryGetValue(aggregateId, out eventCollection))
      {
        eventCollection = new List<IEvent>();
        _events.Add(aggregateId, eventCollection);
      }

      foreach (var @event in events)
      {
        eventCollection.Add(@event);
      }
    }

    public IEnumerable<IEvent> GetEventsForAggregate(Guid aggregateId)
    {
      ICollection<IEvent> eventCollection;
      if (_events.TryGetValue(aggregateId, out eventCollection))
      {
        return eventCollection;
      }
      return Enumerable.Empty<IEvent>();
    }
  }
}
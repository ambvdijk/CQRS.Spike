using System;
using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  public class Aggregate
  {
    private readonly Dictionary<Type, Action<IEvent>> _handlers = new Dictionary<Type, Action<IEvent>>();
    private int _version;

    /// <summary>
    /// The unique ID of the aggregate.
    /// </summary>
    public Guid Id { get; internal set; }

    /// <summary>
    /// Configures a handler for an event. 
    /// </summary>
    protected void Handles<TEvent>(Action<TEvent> handler)
        where TEvent : IEvent
    {
      _handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
    }

    public int EventsLoaded { get; private set; }

    public void LoadFrom(IEnumerable<IEvent> history)
    {
      foreach (var e in history)
      {
        Action<IEvent> handler;
        if (!_handlers.TryGetValue(e.GetType(), out handler))
        {
          throw new InvalidOperationException(String.Format("Aggregate {0} does not know how to apply event {1}", GetType().Name, e.GetType().Name));
        }
        handler.Invoke(e);
        EventsLoaded++;
      }
    }
  }
}
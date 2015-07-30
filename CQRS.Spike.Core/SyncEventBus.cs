using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  /// <summary>
  /// The SyncEventBus short-circuits directly to the EventDispatcher. Usefull for local (debug) testing
  /// </summary>
  public class SyncEventBus : IEventBus
  {
    private readonly IEventDispatcher _dispatcher;


    public SyncEventBus(IEventDispatcher dispatcher)
    {
      _dispatcher = dispatcher;
    }

    public void Publish(IEnumerable<IEvent> events)
    {
      foreach (var @event in events)
      {
        Publish(@event);
      }
    }

    public void Publish(IEvent @event)
  {
    _dispatcher.Dispatch(@event);
  }
  }
}
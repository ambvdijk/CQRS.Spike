using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CQRS.Spike.Core
{
  public class EventDispatcher : IEventDispatcher, IEventHandlerRegistry
  {
    private static readonly Type EventDispatcherType;
    private readonly Dictionary<Type, List<Action<IEvent>>> _handlers;
    private readonly List<IEventSpy> _spies;

    static EventDispatcher()
    {
      EventDispatcherType = typeof (EventDispatcher);
    }

    public EventDispatcher()
    {
      _spies = new List<IEventSpy>();
      _handlers = new Dictionary<Type, List<Action<IEvent>>>();
    }

    public void Dispatch(IEvent @event)
    {
      var eventType = @event.GetType();

      foreach (var eventSpy in _spies)
      {
        eventSpy.Handle(@event);
      }

      List<Action<IEvent>> eventHandlers;
      if (_handlers.TryGetValue(eventType, out eventHandlers))
      {
        foreach (var eventHandler in eventHandlers)
        {
          eventHandler(@event);
        }
      }
    }

    public void Register(IEventHandler handler)
    {
      if (handler == null)
      {
        throw new ArgumentNullException("handler");
      }

      var handlerType = handler.GetType();

      if (typeof (IEventSpy).IsAssignableFrom(handlerType))
      {
        _spies.Add((IEventSpy) handler);
      }

      var eventTypes = handler.GetType()
        .GetInterfaces()
        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof (IEventHandler<>))
        .Select(i => i.GetGenericArguments()[0]);

      foreach (var eventType in eventTypes)
      {
        EventDispatcherType
          .GetMethod("RegisterHandler", BindingFlags.Instance | BindingFlags.NonPublic)
          .MakeGenericMethod(eventType)
          .Invoke(this, new object[] { handler });  
      }
    }

    private void RegisterHandler<TEvent>(IEventHandler<TEvent> handler)
      where TEvent : IEvent
    {
      if (handler == null)
      {
        throw new ArgumentNullException("handler");
      }

      var eventType = typeof (TEvent);

      List<Action<IEvent>> eventHandlers;
      if (!_handlers.TryGetValue(eventType, out eventHandlers))
      {
        eventHandlers = new List<Action<IEvent>>();
        _handlers.Add(eventType, eventHandlers);
      }

      eventHandlers.Add((@event) => handler.Handle((TEvent) @event));
    }
  }
}
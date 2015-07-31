using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  public class EventHandlerAutoRegistrator
  {
    public EventHandlerAutoRegistrator(IEventHandlerRegistry registry, IEnumerable<IEventHandler> handlers)
    {
      foreach (var handler in handlers)
      {
        registry.Register(handler);
      }
    }
  }
}
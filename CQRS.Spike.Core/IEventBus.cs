using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  public interface IEventBus
  {
    void Publish(IEnumerable<IEvent> @events);
    void Publish(IEvent @event);
  }
}

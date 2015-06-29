namespace CQRS.Spike.Core
{
  public interface IEventBus
  {
    void Publish<T>(T @event) where T : IEvent;
  }
}

namespace CQRS.Spike.Core
{
  public interface IEventDispatcher
  {
    void Dispatch(IEvent @event);
  }
}
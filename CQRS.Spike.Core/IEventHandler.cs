namespace CQRS.Spike.Core
{
  public interface IEventHandler
  {

  }

  public interface IEventHandler<in T> : IEventHandler 
    where T:IEvent
  {
    void Handle(T @event);
  }
}
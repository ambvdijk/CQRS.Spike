namespace CQRS.Spike.Core
{
  public interface IEventHandlerRegistry
  {
    void Register(IEventHandler handler);
  }
}
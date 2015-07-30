namespace CQRS.Spike.Core
{
  /// <summary>
  /// Special type of IEventHandler interested in all events!
  /// Usefull for logging etc...
  /// </summary>
  public interface IEventSpy : IEventHandler
  {
    void Handle(IEvent @event);
  }
}
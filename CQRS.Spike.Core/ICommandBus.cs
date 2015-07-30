namespace CQRS.Spike.Core
{
  public interface ICommandBus
  {
    void Send(ICommand command);
  }
}
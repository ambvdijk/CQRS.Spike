namespace CQRS.Spike.Core
{
  public interface ICommandBus
  {
    void Send<T>(T command) where T : ICommand;
  }
}
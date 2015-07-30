namespace CQRS.Spike.Core
{
  public interface ICommandDispatcher
  {
    void Dispatch(ICommand command);
  }
}
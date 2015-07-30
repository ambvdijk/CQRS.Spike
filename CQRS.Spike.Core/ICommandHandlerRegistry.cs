namespace CQRS.Spike.Core
{
  public interface ICommandHandlerRegistry
  {
    void Register(ICommandHandler handler);
  }
}
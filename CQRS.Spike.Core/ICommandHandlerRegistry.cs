namespace CQRS.Spike.Core
{
  public interface ICommandHandlerRegistry
  {
    void Register<TCommand, TAggregate>(ICommandHandler<TAggregate,TCommand> handler)
      where TCommand : ICommand
      where TAggregate : Aggregate, new();
  }
}
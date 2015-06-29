using System.Collections.Generic;

namespace CQRS.Spike.Core
{

  public interface ICommandHandler { }

  public interface ICommandHandler<in TAggregate,in TCommand> : ICommandHandler
    where TAggregate : Aggregate
    where TCommand : ICommand
  {
    IEnumerable<IEvent> Handle(TAggregate aggregate, TCommand command);
  }
}
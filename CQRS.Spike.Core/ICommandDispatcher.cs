using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  public interface ICommandDispatcher
  {
    void Dispatch<TCommand>(TCommand command)
      where TCommand : ICommand;
  }
}
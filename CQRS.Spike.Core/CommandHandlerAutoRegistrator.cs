using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  public class CommandHandlerAutoRegistrator
  {
    public CommandHandlerAutoRegistrator(ICommandHandlerRegistry registry, IEnumerable<ICommandHandler> handlers)
    {
      foreach (var handler in handlers)
      {
        registry.Register(handler);
      }
    }
  }
}
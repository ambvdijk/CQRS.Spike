using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  public class CommandRegistrator
  {
    public CommandRegistrator(ICommandHandlerRegistry registry, IEnumerable<ICommandHandler> handlers)
    {
      foreach (var handler in handlers)
      {
        registry.Register(handler);
      }
    }
  }
}
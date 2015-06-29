using System.Collections.Generic;
using CQRS.Spike.Core;
using CQRS.Spike.Domain.Commands;
using CQRS.Spike.Domain.Events;

namespace CQRS.Spike.Domain.CommandHandlers
{
  public class OpenTabCommandHandler : ICommandHandler<Tab,OpenTab>
  {
    public IEnumerable<IEvent> Handle(Tab tab, OpenTab command)
    {
      yield return new TabOpened
      {
        Id = command.Id,
        Table = command.Table
      };
    }
  }
}

using System;
using System.Collections.Generic;
using CQRS.Spike.Core;
using CQRS.Spike.Domain.Commands;
using CQRS.Spike.Domain.Events;

namespace CQRS.Spike.Domain.CommandHandlers
{
  public class CloseTabCommandHandler : ICommandHandler<Tab,CloseTab>
  {
    public IEnumerable<IEvent> Handle(Tab aggregate, CloseTab command)
    {
      if (!aggregate.Open)
      {
        throw new InvalidOperationException("Tab already closed!");
      }

      yield return new TabClosed
      {
        Id = aggregate.Id
      };
    }
  }
}
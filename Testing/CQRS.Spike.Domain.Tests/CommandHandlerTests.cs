using System;
using System.Linq;
using CQRS.Spike.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Spike.Domain.Tests
{
  public abstract class CommandHandlerTests<TAggregate,THandler,TCommand>
    where TAggregate : Aggregate, new() where TCommand : ICommand
    where THandler: class, ICommandHandler<TAggregate,TCommand>, new()
  {
    protected TAggregate Aggregate { get; private set; }
    protected THandler Handler { get; private set; }

    [TestInitialize]
    public void Setup()
    {
      Aggregate = new TAggregate
      {
        Id = Guid.NewGuid()
      };

      Handler = new THandler();
    }

    [TestCleanup]
    public void Cleanup()
    {
      Aggregate = null;
      Handler = null;
    }

    protected IEvent[] Handle(TCommand command)
    {
      return Handler.Handle(Aggregate, command).ToArray();
    }

    protected void LoadHistory(params IEvent[] events)
    {
      Aggregate.LoadFrom(events);
    }

  }
}
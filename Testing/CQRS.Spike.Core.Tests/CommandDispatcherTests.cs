using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CQRS.Spike.Core.Tests
{
  [TestClass]
  public class CommandDispatcherTests
  {
    [TestMethod]
    public void CommandDispatcherCanDispatchCommand()
    {
      var aggregateId = Guid.NewGuid();

      var eventStore = new Mock<IEventStore>(MockBehavior.Strict);

      eventStore
        .Setup(e => e.GetEventsForAggregate(aggregateId))
        .Returns(Enumerable.Empty<IEvent>());

      eventStore
        .Setup(e => e.SaveEvents(aggregateId, It.IsAny<IEnumerable<IEvent>>(), 0));
        

      var dispatcher = new CommandDispatcher(eventStore.Object);
      dispatcher.Register(new TestCommandHandler());
      dispatcher.Dispatch(new TestCommand {Id = aggregateId});

      eventStore.VerifyAll();
    }

    [TestMethod]
    public void CommandDispatcherThrowsOnUnregisteredCommand()
    {
      var eventStore = new Mock<IEventStore>(MockBehavior.Strict);

      var dispatcher = new CommandDispatcher(eventStore.Object);

      ExceptionAssert.Throws<InvalidOperationException>
      (
        () => dispatcher.Dispatch(new TestCommand())
      );
    }

    [TestMethod]
    public void CommandDispatcherThrowsOnDuplicateRegisteredCommand()
    {
      var eventStore = new Mock<IEventStore>(MockBehavior.Strict);

      var dispatcher = new CommandDispatcher(eventStore.Object);

      dispatcher.Register(new TestCommandHandler());

      ExceptionAssert.Throws<InvalidOperationException>
      (
        () => dispatcher.Register(new TestCommandHandler())
      );
    }

    public class TestAggregate : Aggregate
    {

    }

    public class TestCommand : Command
    {

    }

    public class TestCommandHandler : ICommandHandler<TestAggregate, TestCommand>
    {
      public IEnumerable<IEvent> Handle(TestAggregate aggregate, TestCommand command)
      {
        yield return new CommandTested();
      }
    }

    public class CommandTested : Event
    {
    }
  }
}
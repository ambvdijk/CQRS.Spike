using System;
using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  public class CommandDispatcher : ICommandDispatcher, ICommandHandlerRegistry
  {
    private readonly IEventStore _eventStore;
    private readonly IDictionary<Type, Action<ICommand>> _handlers;

    public CommandDispatcher(IEventStore eventStore)
    {
      _eventStore = eventStore;
      _handlers = new Dictionary<Type, Action<ICommand>>();
    }

    public void Dispatch<TCommand>(TCommand command) where TCommand : ICommand
    {
      var commandType = typeof (TCommand);

      Action<ICommand> handler;
      if (!_handlers.TryGetValue(commandType, out handler))
      {
        throw new InvalidOperationException("No handler registered for command: " + commandType);
      }

      handler(command);
    }

    public void Register<TCommand, TAggregate>(ICommandHandler<TAggregate, TCommand> handler)
      where TCommand : ICommand
      where TAggregate : Aggregate, new()
    {
      if (handler == null)
      {
        throw new ArgumentNullException("handler");
      }

      var commandType = typeof (TCommand);

      if (_handlers.ContainsKey(commandType))
      {
        throw new InvalidOperationException("Command already registered: " + commandType);
      }

      _handlers.Add
        (
          commandType,
          (command) =>
          {
            var aggregate = new TAggregate
            {
              Id = command.Id
            };

            aggregate.LoadFrom(_eventStore.GetEventsForAggregate(command.Id));

            var events = handler.Handle(aggregate, (TCommand) command);

            //TODO: Publish events to queue / bus
            _eventStore.SaveEvents(command.Id, events, aggregate.EventsLoaded);
          }
        );
    }
  }
}
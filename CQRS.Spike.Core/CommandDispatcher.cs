using System;
using System.Collections.Generic;
using System.Linq;

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

    //public IEnumerable<IEvent> Dispatch(ICommand command)
    //{
    //  var commandType = command.GetType();
    //  ICommandHandler handler = null;

    //  if (_handlers.TryGetValue(commandType, out handler))
    //  {
    //    return ((dynamic)handler).Handle((dynamic)command);
    //  }

    //  // There can be a generic logging/tracing/auditing handlers
    //  if (_handlers.TryGetValue(typeof(ICommand), out handler))
    //  {
    //    return ((dynamic)handler).Handle((dynamic)command);
    //  }

    //  return new[]
    //  {
    //    new UnhandledCommand
    //    {
    //      Id = command.Id,
    //      Name = commandType.Name
    //    }
    //  };
    //}

    //public void Register(ICommandHandler handler)
    //{
    //  var genericHandler = typeof(ICommandHandler<>);
    //  var supportedCommandTypes = handler.GetType()
    //    .GetInterfaces()
    //    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericHandler)
    //    .Select(i => i.GetGenericArguments()[0])
    //    .ToList();

    //  if (_handlers.Keys.Any(supportedCommandTypes.Contains))
    //  {
    //    throw new InvalidOperationException("The command handled by the received handler already has a registered handler.");
    //  }

    //  // Register this handler for each of he handled types.
    //  foreach (var commandType in supportedCommandTypes)
    //  {
    //    _handlers.Add(commandType, handler);
    //  }
    //}
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

            //TODO: Publish events
            _eventStore.SaveEvents(command.Id, events, aggregate.EventsLoaded);
          }
        );
    }
  }
}
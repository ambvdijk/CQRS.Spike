using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CQRS.Spike.Core
{
  public class CommandDispatcher : ICommandDispatcher, ICommandHandlerRegistry
  {
    private static readonly Type CommandDispatcherType;
    private readonly IEventStore _eventStore;
    private readonly IEventBus _eventBus;
    private readonly IDictionary<Type, Action<ICommand>> _handlers;
    private readonly IEventMigrator[] _migrators;

    static CommandDispatcher()
    {
      CommandDispatcherType = typeof (CommandDispatcher);
    }

    public CommandDispatcher(IEventStore eventStore, IEventBus eventBus, IEnumerable<IEventMigrator> migrators = null)
    {
      if (eventStore == null)
      {
        throw new ArgumentNullException("eventStore");
      }

      if (eventBus == null)
      {
        throw new ArgumentNullException("eventBus");
      }

      _eventStore = eventStore;
      _eventBus = eventBus;

      _handlers = new Dictionary<Type, Action<ICommand>>();
      _migrators = ToArray(migrators);
    }

    private IEventMigrator[] ToArray(IEnumerable<IEventMigrator> migrators)
    {
      if (migrators == null)
      {
        return new IEventMigrator[0];
      }

      return migrators.ToArray();
    }

    public void Dispatch(ICommand command)
    {
      var commandType = command.GetType();

      Action<ICommand> handler;
      if (!_handlers.TryGetValue(commandType, out handler))
      {
        throw new InvalidOperationException("No handler registered for command: " + commandType);
      }

      handler(command);
    }

    public void Register(ICommandHandler handler)
    {
      if (handler == null)
      {
        throw new ArgumentNullException("handler");
      }

      var handlerType = handler.GetType()
        .GetInterfaces()
        .Single(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof (ICommandHandler<,>));

      CommandDispatcherType
        .GetMethod("RegisterHandler",BindingFlags.Instance | BindingFlags.NonPublic)
        .MakeGenericMethod(handlerType.GenericTypeArguments)
        .Invoke(this, new object[] {handler});
    }

    private void RegisterHandler<TAggregate,TCommand>(ICommandHandler<TAggregate, TCommand> handler)
      where TAggregate : Aggregate, new()
      where TCommand : ICommand
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

            aggregate.LoadFrom(GetMigratedEventStream(command.Id));

            var events = handler
              .Handle(aggregate, (TCommand) command)
              .ToArray();

            _eventStore.SaveEvents(aggregate.Id, events,command.OriginalVersion);

            _eventBus.Publish(events);
          }
        );
    }

    private IEnumerable<IEvent> GetMigratedEventStream(Guid aggregateId)
    {
      return _migrators.Aggregate(_eventStore.GetEventsForAggregate(aggregateId), (events, migrator) => migrator.Migrate(events));
    }
  }

}
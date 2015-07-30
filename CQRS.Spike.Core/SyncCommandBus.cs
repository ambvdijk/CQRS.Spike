namespace CQRS.Spike.Core
{
  /// <summary>
  /// The SyncCommandBus short-circuits directly to the CommandDispatcher. Usefull for local (debug) testing
  /// </summary>
  public class SyncCommandBus : ICommandBus
  {
    private readonly ICommandDispatcher _dispatcher;

    public SyncCommandBus(ICommandDispatcher dispatcher)
    {
      _dispatcher = dispatcher;
    }

    public void Send(ICommand command)
    {
      _dispatcher.Dispatch(command);
    }
  }
}

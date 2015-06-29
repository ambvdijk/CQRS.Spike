using CQRS.Spike.Core;

namespace CQRS.Spike.Infra
{

  public class SyncCommandBus : ICommandBus
  {
    private readonly ICommandDispatcher _dispatcher;

    public SyncCommandBus(ICommandDispatcher dispatcher)
    {
      _dispatcher = dispatcher;
    }

    public void Send<T>(T command) where T : ICommand
    {
      _dispatcher.Dispatch(command);
    }
  }
}

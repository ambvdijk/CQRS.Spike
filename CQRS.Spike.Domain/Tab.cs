using CQRS.Spike.Core;
using CQRS.Spike.Domain.Events;

namespace CQRS.Spike.Domain
{
  public class Tab : Aggregate
  {
    private bool _open;

    public Tab()
    {
      Handles<TabOpened>(OnTabOpened);
    }

    public bool Open 
    {
      get { return _open; }
    }

    public void OnTabOpened(TabOpened tabOpened)
    {
      _open = true;
    }
  }
}

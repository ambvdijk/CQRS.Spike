using CQRS.Spike.Core;

namespace CQRS.Spike.Domain.Events
{
  public class TabOpened : Event
  {
    public int Table { get; set; }
  }
}

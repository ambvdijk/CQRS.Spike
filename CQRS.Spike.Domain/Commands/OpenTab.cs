using CQRS.Spike.Core;

namespace CQRS.Spike.Domain.Commands
{
  public class OpenTab : Command
  {
    public int Table { get; set; }
  }
}

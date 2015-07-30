using System;

namespace CQRS.Spike.Core
{
  public class Command : ICommand
  {
    public Guid Id { get; set; }
    public int OriginalVersion { get; set; }
  }
}
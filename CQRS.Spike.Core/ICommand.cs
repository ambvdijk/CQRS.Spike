using System;

namespace CQRS.Spike.Core
{
  public interface ICommand
  {
    Guid Id { get; set; }
    int OriginalVersion { get; }
  }
}
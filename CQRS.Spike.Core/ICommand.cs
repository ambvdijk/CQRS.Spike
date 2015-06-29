using System;

namespace CQRS.Spike.Core
{
  public interface ICommand
  {
    Guid Id { get; set; }
  }

  public class Command : ICommand
  {
    public Guid Id { get; set; }
  }
}
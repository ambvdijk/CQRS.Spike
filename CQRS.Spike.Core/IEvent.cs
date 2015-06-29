using System;

namespace CQRS.Spike.Core
{
  public interface IEvent
  {
    Guid Id { get; }
    int Version { get; }
  }

  public class Event : IEvent
  {
    public Guid Id { get; set; }
    public int Version { get; set; }
  }
}
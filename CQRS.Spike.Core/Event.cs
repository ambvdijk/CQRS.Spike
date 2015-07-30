using System;

namespace CQRS.Spike.Core
{
  public class Event : IEvent
  {
    public Guid Id { get; set; }
    public int Version { get; set; }
  }
}
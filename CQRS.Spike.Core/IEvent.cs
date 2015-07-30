using System;

namespace CQRS.Spike.Core
{
  public interface IEvent
  {
    Guid Id { get; }
    int Version { get; }
  }
}
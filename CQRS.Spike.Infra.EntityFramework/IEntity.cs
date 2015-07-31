using System;

namespace CQRS.Spike.Infra.EntityFramework
{
  public interface IEntity
  {
    Guid Id { get; }
    int Version { get; }
  }
}
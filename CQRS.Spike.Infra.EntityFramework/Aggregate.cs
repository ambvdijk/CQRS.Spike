using System;
using System.Collections.Generic;

namespace CQRS.Spike.Infra.EntityFramework
{
  public class Aggregate : IEntity
  {
    public Guid Id { get; set; }
    public int Version { get; set; }

    public ICollection<Event> Events { get; set; }
  }
}
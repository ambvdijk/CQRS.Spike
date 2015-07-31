using System;

namespace CQRS.Spike.Infra.EntityFramework
{
  public class Event : IEntity
  {
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string Data { get; set; }
  }
}
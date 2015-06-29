using System;
using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  public interface IAggregateFactory<out T> where T : Aggregate
  {
    T Build(Guid id, IEnumerable<IEvent> events);
  }
}
using System;
using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  public interface IEventStore
  {
    void SaveEvents(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion);
    IEnumerable<IEvent> GetEventsForAggregate(Guid aggregateId);
  }
}
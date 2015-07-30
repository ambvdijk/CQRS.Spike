using System.Collections.Generic;

namespace CQRS.Spike.Core
{
  public interface IEventMigrator
  {
    IEnumerable<IEvent> Migrate(IEnumerable<IEvent> stream);
  }
}

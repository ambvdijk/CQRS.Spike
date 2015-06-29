using System;

namespace CQRS.Spike.Core
{
  public interface IRepository<T>
    where T : Aggregate
  {
    void Save(T aggregate);
    T GetById(Guid id);
  }
}
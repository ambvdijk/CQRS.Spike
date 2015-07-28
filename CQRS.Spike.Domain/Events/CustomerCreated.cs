using CQRS.Spike.Core;
using CQRS.Spike.Domain.Models;

namespace CQRS.Spike.Domain.Events
{
  public class CustomerCreated : Event
  {
    public CompanyDetails Company { get; set; }
  }
}
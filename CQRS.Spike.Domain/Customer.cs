using CQRS.Spike.Core;
using CQRS.Spike.Domain.Events;
using CQRS.Spike.Domain.Models;

namespace CQRS.Spike.Domain
{
  public class Customer : Aggregate
  {
    public Customer()
    {
      Handles<CustomerCreated>(OnCustomerCreated);
    }

    private void OnCustomerCreated(CustomerCreated @event)
    {
      Details = @event.Company;
    }

    private CompanyDetails Details { get; set; }
  }
}
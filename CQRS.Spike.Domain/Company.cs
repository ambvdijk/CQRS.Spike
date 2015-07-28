using CQRS.Spike.Core;
using CQRS.Spike.Domain.Events;
using CQRS.Spike.Domain.Models;

namespace CQRS.Spike.Domain
{
  public class Company : Aggregate
  {
    private CompanyDetails Details { get; set; }

    public Company()
    {
      Handles<CompanyCreated>(OnCompanyCreated);
    }

    private void OnCompanyCreated(CompanyCreated @event)
    {
      Details = @event.Company;
    }
  }
}
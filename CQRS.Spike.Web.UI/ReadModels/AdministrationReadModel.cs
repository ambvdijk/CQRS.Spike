using System;
using System.Collections.Generic;
using CQRS.Spike.Core;
using CQRS.Spike.Domain;
using CQRS.Spike.Domain.Events;
using CQRS.Spike.Domain.Models;

namespace CQRS.Spike.Web.UI.ReadModels
{
  public class AdministrationReadModel :
    IEventHandler<CompanyCreated>,
    IAdministrationRepository
  {
    private readonly Dictionary<Guid, Aggregate> _companies;

    public AdministrationReadModel()
    {
      _companies = new Dictionary<Guid, Aggregate>();
    }

    public void Handle(CompanyCreated @event)
    {
      _companies.Add(@event.Id, new Company());
    }

    public IEnumerable<CompanyDetails> Companies
    {
      get { return  }
    }
  }

  public interface IAdministrationRepository
  {
    IEnumerable<CompanyDetails> Companies { get; }
  }
}
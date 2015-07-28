using System;
using System.Collections.Generic;
using CQRS.Spike.Core;
using CQRS.Spike.Domain.Commands;
using CQRS.Spike.Domain.Events;

namespace CQRS.Spike.Domain.CommandHandlers
{
  public class CreateCompanyCommandHandler : ICommandHandler<Company, CreateCompany>
  {
    public IEnumerable<IEvent> Handle(Company aggregate, CreateCompany command)
    {
      if (command.Company == null)
      {
        throw new InvalidOperationException("Company details are missing");
      }

      if (String.IsNullOrEmpty(command.Company.Name))
      {
        throw new InvalidOperationException("Company name is mandatory");
      }

      yield return new CompanyCreated
      {
        Id = command.Id,
        Company = command.Company
      };
    }
  }
}
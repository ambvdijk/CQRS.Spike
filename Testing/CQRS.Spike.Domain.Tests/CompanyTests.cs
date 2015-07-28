using System;
using CQRS.Spike.Core.Tests;
using CQRS.Spike.Domain.CommandHandlers;
using CQRS.Spike.Domain.Commands;
using CQRS.Spike.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Spike.Domain.Tests
{
  [TestClass]
  public class CreateCompanyCommandHandlerTests : CommandHandlerTests<Company,CreateCompanyCommandHandler,CreateCompany>
  {
    [TestMethod]
    public void CreateCompanyCommandHandler_WillThrowOnEmptyDetails()
    {
      ExceptionAssert.Throws<InvalidOperationException>(() => Handle(new CreateCompany()));
    }

    [TestMethod]
    public void CreateCompanyCommandHandler_WillThrowOnEmptyName()
    {
      ExceptionAssert.Throws<InvalidOperationException>(() => Handle(new CreateCompany{Company = new CompanyDetails()}));
    }
  }
}

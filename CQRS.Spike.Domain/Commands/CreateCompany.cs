using CQRS.Spike.Core;
using CQRS.Spike.Domain.Models;

namespace CQRS.Spike.Domain.Commands
{
  public class CreateCompany : Command
  {
    public CompanyDetails Company { get; set; }
  }
}
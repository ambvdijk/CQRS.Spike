using System;
using CQRS.Spike.Core;

namespace CQRS.Spike.Domain.Commands
{
  public class OpenProject : Command
  {
    public Guid Customer { get; set; }
    public Guid PayRoller { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal HourlyRate { get; set; }
  }
}
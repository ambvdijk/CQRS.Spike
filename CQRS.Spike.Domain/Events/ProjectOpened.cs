using System;
using CQRS.Spike.Core;
using CQRS.Spike.Domain.Models;

namespace CQRS.Spike.Domain.Events
{
  public class ProjectOpened : Event
  {
    public CompanyDetails Customer { get; set; }
    public CompanyDetails PayRoller { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal HourlyRate { get; set; }
  }
}
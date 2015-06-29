using System;

namespace CQRS.Spike.Web.UI.Models
{
  public abstract class ViewModel
  {
    protected ViewModel()
    {
      Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
  }
}
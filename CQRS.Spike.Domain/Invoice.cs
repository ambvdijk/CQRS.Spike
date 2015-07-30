using CQRS.Spike.Core;
using CQRS.Spike.Domain.Events;

namespace CQRS.Spike.Domain
{
  public class Invoice : Aggregate
  {
    public Invoice()
    {
      Handles<InvoiceOpened>(OnInvoiceOpened);
      Handles<InvoiceSubmitted>(OnInvoiceSubmitted);
      Handles<InvoicePayed>(OnInvoicePayed);
    }

    public bool IsPayed { get; private set; }
    public bool IsSubmitted { get; private set; }

    private void OnInvoiceOpened(InvoiceOpened obj)
    {

    }

    private void OnInvoicePayed(InvoicePayed @event)
    {
      IsPayed = true;
    }

    private void OnInvoiceSubmitted(InvoiceSubmitted @event)
    {
      IsSubmitted = true;
    }
  }
}
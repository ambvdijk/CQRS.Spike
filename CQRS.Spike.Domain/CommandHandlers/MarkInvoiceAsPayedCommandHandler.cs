using System;
using System.Collections.Generic;
using CQRS.Spike.Core;
using CQRS.Spike.Domain.Commands;
using CQRS.Spike.Domain.Events;

namespace CQRS.Spike.Domain.CommandHandlers
{
  public class MarkInvoiceAsPayedCommandHandler : ICommandHandler<Invoice, MarkInvoiceAsPayed>
  {
    public IEnumerable<IEvent> Handle(Invoice aggregate, MarkInvoiceAsPayed command)
    {
      if (aggregate.IsPayed)
      {
        throw new InvalidOperationException("Invoice has already been payed!");
      }

      yield return new InvoicePayed
      {
        Id = command.Id
      };
    }
  }
}
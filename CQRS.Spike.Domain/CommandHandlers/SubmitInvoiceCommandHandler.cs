using System;
using System.Collections.Generic;
using CQRS.Spike.Core;
using CQRS.Spike.Domain.Commands;
using CQRS.Spike.Domain.Events;

namespace CQRS.Spike.Domain.CommandHandlers
{
  public class SubmitInvoiceCommandHandler : ICommandHandler<Invoice,SubmitInvoice>
  {
    public IEnumerable<IEvent> Handle(Invoice aggregate, SubmitInvoice command)
    {
      if (aggregate.IsSubmitted)
      {
        throw new InvalidOperationException("Invoice has already been submitted!");
      }

      yield return new InvoiceSubmitted
      {
        Id = command.Id
      };
    }
  }
}
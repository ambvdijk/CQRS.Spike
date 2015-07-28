using System.Collections.Generic;
using CQRS.Spike.Core;
using CQRS.Spike.Domain.Commands;
using CQRS.Spike.Domain.Events;

namespace CQRS.Spike.Domain.CommandHandlers
{
  public class OpenInvoiceCommandHandler : ICommandHandler<Invoice,OpenInvoice>
  {
    public IEnumerable<IEvent> Handle(Invoice aggregate, OpenInvoice command)
    {
      yield return new InvoiceOpened
      {
        Id = command.Id
      };
    }
  }
}

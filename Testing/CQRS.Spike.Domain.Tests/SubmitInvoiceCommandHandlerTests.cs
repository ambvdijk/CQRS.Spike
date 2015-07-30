using System;
using CQRS.Spike.Core.Tests;
using CQRS.Spike.Domain.CommandHandlers;
using CQRS.Spike.Domain.Commands;
using CQRS.Spike.Domain.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Spike.Domain.Tests
{
  [TestClass]
  public class SubmitInvoiceCommandHandlerTests :
    CommandHandlerTests<Invoice, SubmitInvoiceCommandHandler, SubmitInvoice>
  {
    [TestMethod]
    public void SubmitInvoiceCommandHandler_CanSubmitOpenInvoice()
    {
      LoadHistory(new InvoiceOpened());

      var events = Handle(new SubmitInvoice());

      Assert.AreEqual(1,events.Length);
      Assert.IsInstanceOfType(events[0],typeof(InvoiceSubmitted));
    }

    [TestMethod]
    public void SubmitInvoiceCommandHandler_WillThrowWhenAlreadySubmitted()
    {
      LoadHistory(new InvoiceOpened(),new InvoiceSubmitted());

      ExceptionAssert.Throws<InvalidOperationException>(() => Handle(new SubmitInvoice()));
    }
  }
}
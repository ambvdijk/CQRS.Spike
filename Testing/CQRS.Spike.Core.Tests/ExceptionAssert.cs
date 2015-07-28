using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Spike.Core.Tests
{
  public static class ExceptionAssert
  {
    public static void Throws<T>(Action action)
      where T : Exception
    {
      if (action == null)
      {
        throw new ArgumentNullException("action");
      }

      var exceptionType = typeof (T);

      try
      {
        action();
      }
      catch (T)
      {
        return;
      }
      catch (Exception ex)
      {
        Assert.Fail("Exception of type {0} was thrown, expected {1} to be thrown", ex.GetType(), exceptionType);
      }

      Assert.Fail("Expected exception of type {0} to be thrown", exceptionType);

    }
  }
}
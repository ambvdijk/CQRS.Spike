using System;
using System.Web;

namespace CQRS.Spike.Web.UI.Extensions
{
  public static class HttpRequestBaseExtensions
  {
    public static bool IsHttpGet(this HttpRequestBase request)
    {
      if (request == null)
      {
        throw new ArgumentNullException("request");
      }

      return String.Equals(request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsHttpPost(this HttpRequestBase request)
    {
      if (request == null)
      {
        throw new ArgumentNullException("request");
      }

      return String.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase);
    }
  }
}
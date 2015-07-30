using System;
using System.Configuration;

namespace CQRS.Spike.Core.Configuration
{
  public class ConnectionConfiguration : IConnectionConfiguration
  {
    public IConnectionString RequireConnection(string name)
    {
      if (name == null)
      {
        throw new ArgumentNullException("name");
      }

      return ConfigurationManager.ConnectionStrings.Require(name);
    }
  }
}
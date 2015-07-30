using System;
using System.Configuration;

namespace CQRS.Spike.Core.Configuration
{
  public static class ConnectionStringSettingsCollectionExtensions
  {
    public static IConnectionString Require(this ConnectionStringSettingsCollection settings, string name)
    {
      if (settings == null)
      {
        throw new ArgumentNullException("settings");
      }

      if (name == null)
      {
        throw new ArgumentNullException("name");
      }

      var connectionString = settings[name];

      if (connectionString == null)
      {
        throw new ConfigurationErrorsException("Required connection string configuration is missing: " + name);
      }

      return new ConnectionStringWrapper(connectionString);
    }
  }
}
using System.Configuration;

namespace CQRS.Spike.Core.Configuration
{
  public class ConnectionStringWrapper : IConnectionString
  {
    private readonly ConnectionStringSettings _setting;

    public ConnectionStringWrapper(ConnectionStringSettings setting)
    {
      _setting = setting;
    }

    public string ConnectionString
    {
      get { return _setting.ConnectionString; }
    }

    public string ProviderName
    {
      get { return _setting.ProviderName; }
    }

    public string Name
    {
      get { return _setting.Name; }
    }
  }
}
namespace CQRS.Spike.Core.Configuration
{
  public interface IConnectionString
  {
    string ConnectionString { get; }
    string ProviderName { get; }
    string Name { get; }
  }
}
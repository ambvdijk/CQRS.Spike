namespace CQRS.Spike.Core.Configuration
{
  public interface IConnectionConfiguration
  {
    IConnectionString RequireConnection(string name);
  }
}
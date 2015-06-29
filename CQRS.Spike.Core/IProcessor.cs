namespace CQRS.Spike.Core
{
  public interface IProcessor
  {
    void Start();
    void Stop();
  }
}
using System.IO;

namespace CQRS.Spike.Core.Serialization
{
  public interface ITextSerializer
  {
    void Serialize(TextWriter writer, object graph);
    object Deserialize(TextReader reader);
  }
}


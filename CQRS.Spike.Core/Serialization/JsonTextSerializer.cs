using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CQRS.Spike.Core.Serialization
{
  public class JsonTextSerializer : ITextSerializer
  {
    private readonly JsonSerializer _serializer;

    public JsonTextSerializer() : this(CreateDefaultSerializer())
    {

    }

    public JsonTextSerializer(JsonSerializer serializer)
    {
      _serializer = serializer;
    }

    private static JsonSerializer CreateDefaultSerializer()
    {
      return JsonSerializer.Create
        (
          new JsonSerializerSettings
          {
            // Allows deserializing to the actual runtime type
            TypeNameHandling = TypeNameHandling.All,
            // In a version resilient way
            TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
          }
        );
    }



    public void Serialize(TextWriter writer, object graph)
    {
      var jsonWriter = new JsonTextWriter(writer)
      {
        Formatting = Formatting.Indented
      };

      _serializer.Serialize(jsonWriter, graph);

      // We don't close the stream as it's owned by the message.
      writer.Flush();
    }

    public object Deserialize(TextReader reader)
    {
      var jsonReader = new JsonTextReader(reader);

      try
      {
        return _serializer.Deserialize(jsonReader);
      }
      catch (JsonSerializationException e)
      {
        // Wrap in a standard .NET exception.
        throw new SerializationException(e.Message, e);
      }
    }
  }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using CQRS.Spike.Core;
using CQRS.Spike.Core.Configuration;
using CQRS.Spike.Core.Serialization;

namespace CQRS.Spike.Infra.SQL
{
    public class SqlEventStore : IEventStore
    {
      private readonly ITextSerializer _serializer;
      private readonly IConnectionConfiguration _connectionConfiguration;
      private readonly string _connectionName;

      public SqlEventStore(ITextSerializer serializer, IConnectionConfiguration connectionConfiguration, string connectionName)
      {
        if (serializer == null)
        {
          throw new ArgumentNullException("serializer");
        }

        if (connectionConfiguration == null)
        {
          throw new ArgumentNullException("connectionConfiguration");
        }

        if (connectionName == null)
        {
          throw new ArgumentNullException("connectionName");
        }

        _serializer = serializer;
        _connectionConfiguration = connectionConfiguration;
        _connectionName = connectionName;
      }

      public void SaveEvents(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion)
      {
        //TODO: Validate expected version!

        using (var connection = new SqlConnection(_connectionConfiguration.RequireConnection(_connectionName).ConnectionString))
        {
          connection.Open();

          var command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "INSERT INTO Events (Id, Data, Sequence) VALUES (@Id, @Data,@Sequence)";

          int sequence = expectedVersion + 1;

          foreach (var @event in events)
          {
            command.Parameters.Clear();
            command.Parameters.AddWithValue("Id", @event.Id);
            command.Parameters.AddWithValue("Data", Serialize(@event));
            command.Parameters.AddWithValue("Sequence", sequence);

            command.ExecuteNonQuery();
            sequence++;
          }
        }
      }

      private string Serialize(IEvent @event)
      {
        var builder = new StringBuilder();
        using (var textWriter = new StringWriter(builder))
        {
          _serializer.Serialize(textWriter,@event);
        }

        return builder.ToString();
      }

      public IEnumerable<IEvent> GetEventsForAggregate(Guid aggregateId)
      {
        using (var connection = new SqlConnection(_connectionConfiguration.RequireConnection(_connectionName).ConnectionString))
        {
          connection.Open();

          var command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT Id, Data, Sequence FROM Events WHERE Id = @Id ORDER BY Sequence";
          command.Parameters.AddWithValue("Id", aggregateId);

          using (var reader = command.ExecuteReader())
          {
            var ordinal = reader.GetOrdinal("Data");

            while (reader.Read())
            {
              yield return Deserialize(reader.GetString(ordinal));
            }
          }
        }
      }

      private IEvent Deserialize(string graph)
      {
        using (var reader = new StringReader(graph))
        {
          return (IEvent) _serializer.Deserialize(reader);
        }
      }
    }
}

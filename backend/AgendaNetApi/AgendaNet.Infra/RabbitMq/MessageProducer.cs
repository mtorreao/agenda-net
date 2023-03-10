using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using AgendaNet.Domain.Messages;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace AgendaNet.Infra.RabbitMq;

[ExcludeFromCodeCoverage]
public class MessageProducer : IMessageProducer
{
  private readonly ILogger<MessageProducer> _logger;
  private readonly string _connectionString;

  public MessageProducer(ILogger<MessageProducer> logger, string connectionString)
  {
    _logger = logger;
    _connectionString = connectionString;
  }

  public void Publish<T>(T message) where T : Message
  {
    try
    {

      var factory = new ConnectionFactory()
      {
        Uri = new System.Uri(_connectionString)
      };
      using var conn = factory.CreateConnection();
      using var channel = conn.CreateModel();
      channel.QueueDeclare(queue: "agenda-net-create-contact",
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);
      var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

      channel.BasicPublish(exchange: "",
                           routingKey: "agenda-net-create-contact",
                           basicProperties: null,
                           body: body);
    }
    catch (System.Exception exc)
    {
      this._logger.LogError("Error publishing message", exc);
      throw;
    }
  }
}

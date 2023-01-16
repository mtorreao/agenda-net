using System.Text;
using System.Text.Json;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Messages;
using AgendaNet.Domain.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AgendaNet.Infra.RabbitMq;

public class CreateContactConsumer : BackgroundService
{
  private readonly ILogger<CreateContactConsumer> _logger;
  private readonly IRepository _repository;

  public CreateContactConsumer(ILogger<CreateContactConsumer> logger, IRepository repository)
  {
    _logger = logger;
    _repository = repository;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _logger.LogInformation("Aguardando mensagens...");
    try
    {
      var factory = new ConnectionFactory()
      {
        HostName = "localhost",
        UserName = "guest",
        Password = "guest"
      };
      using var conn = factory.CreateConnection();
      using var channel = conn.CreateModel();
      channel.QueueDeclare(queue: "agenda-net-create-contact",
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += MessageReceived;
      channel.BasicConsume(queue: "agenda-net-create-contact",
                autoAck: true,
                consumer: consumer);

      // Aguarda 10 segundos para verificar se o serviço continua ativo
      while (!stoppingToken.IsCancellationRequested)
      {
        _logger.LogInformation($"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
      }
    }
    catch (System.Exception ex)
    {
      _logger.LogError(ex, "Error consuming message");
      throw;
    }
  }

  private void MessageReceived(object sender, BasicDeliverEventArgs e)
  {
    _logger.LogInformation($"[Nova mensagem | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " + Encoding.UTF8.GetString(e.Body.ToArray()));

    var message = JsonSerializer.Deserialize<CreateContactMessage>(Encoding.UTF8.GetString(e.Body.ToArray()));

    _repository.Contacts.Create(new Contact(message!.Name!, message.Email!, message.Phone!, Guid.Parse(message.UserId!)));

    _logger.LogInformation($"[Contato criado no banco | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " + message.Email);

    // Enviar evento para atualizar Redis
  }
}
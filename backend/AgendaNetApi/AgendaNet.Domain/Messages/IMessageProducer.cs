namespace AgendaNet.Domain.Messages
{
  public interface IMessageProducer
  {
    void Publish<T>(T message) where T : Message;
  }
}
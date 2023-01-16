namespace AgendaNet.Domain.Messages
{
  public class CreateContactMessage : Message
  {
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? UserId { get; set; }
  }
}
namespace AgendaNet.Domain.Entities
{
  public class Contact : Entity
  {
    public Contact(string name, string email, string phone, DateTime createdAt, string user)
    {
      Name = name;
      Email = email;
      Phone = phone;
      CreatedAt = createdAt;
      User = user;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string User { get; private set; }
  }
}
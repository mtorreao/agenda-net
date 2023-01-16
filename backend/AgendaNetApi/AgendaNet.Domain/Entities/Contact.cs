namespace AgendaNet.Domain.Entities
{
  public class Contact : Entity
  {
    
    public Contact(string name, string email, string phone, Guid userId)
    {
      Name = name;
      Email = email;
      Phone = phone;
      CreatedAt = new DateTime();
      UserId = userId;
    }

    public Contact(string name, string email, string phone, DateTime createdAt, Guid userId)
    {
      Name = name;
      Email = email;
      Phone = phone;
      CreatedAt = createdAt;
      UserId = userId;
    }

    public void Update(string name, string email, string phone)
    {
      Name = name;
      Email = email;
      Phone = phone;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid UserId { get; private set; }
  }
}
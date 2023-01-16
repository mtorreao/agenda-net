using Flunt.Notifications;

namespace AgendaNet.Domain.Entities
{
  public abstract class Entity : Notifiable<Notification>, IEquatable<Entity>
  {
    protected Entity()
    {
      Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
      Id = id;
    }
    
    public Guid Id { get; private set; }

    public bool Equals(Entity? other)
    {
      return other != null && Id.Equals(other.Id);
    }
  }
}

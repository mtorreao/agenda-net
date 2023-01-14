using AgendaNet.Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace AgendaNet.Infra
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Ignore<Notification>();
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

      modelBuilder.Entity<Contact>().Property(x => x.Name).HasMaxLength(100);
      modelBuilder.Entity<Contact>().Property(x => x.Email).HasMaxLength(100);
      modelBuilder.Entity<Contact>().Property(x => x.Phone).HasMaxLength(20);
    }
  }
}
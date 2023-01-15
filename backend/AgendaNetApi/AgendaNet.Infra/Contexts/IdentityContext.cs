using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgendaNet.Infra.JWT;

public class IdentityContext : IdentityDbContext<AuthUser>
{
  public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<AuthUser>(entity =>
    {
      entity.Property(e => e.Email)
          .IsRequired()
          .HasMaxLength(256);
    });
  }
}

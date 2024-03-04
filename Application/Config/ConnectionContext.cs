using Application.Modules.LaborModule.Models;
using Application.Modules.Auth.Models;
using Microsoft.EntityFrameworkCore;
using Application.Utils.BaseEntity;

namespace Application.Config
{
  public class ConnectionContext : DbContext
  {
    public DbSet<User> User { get; set; }
    public DbSet<Labor> Labor { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=master;User Id=sa;Password=Mssql!Passw0rd;encrypt=false;");

    public override int SaveChanges()
    {
      AddTimestamps();
      return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
      AddTimestamps();
      return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void AddTimestamps()
    {
      var entities = ChangeTracker.Entries()
          .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

      foreach (var entity in entities)
      {
        var now = DateTime.UtcNow; // current datetime

        if (entity.State == EntityState.Added)
        {
          ((BaseEntity)entity.Entity).CreatedAt = now;
        }
          ((BaseEntity)entity.Entity).UpdatedAt = now;
      }
    }

  }

}
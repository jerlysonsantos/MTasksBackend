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

    private string _Server;
    private string _Port;
    private string _Database;
    private string _User;
    private string _Password;
    private string _Encrypt;

    public ConnectionContext()
    {
      _Server = Environment.GetEnvironmentVariable("DATABASE_SERVER") ?? "localhost";
      _Port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "1433";
      _Database = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "master";
      _User = Environment.GetEnvironmentVariable("DATABASE_USER") ?? "sa";
      _Password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "password";
      _Encrypt = Environment.GetEnvironmentVariable("DATABASE_ENCRYPT") ?? "false";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer($"Server={_Server},{_Port};Database={_Database};User Id={_User};Password={_Password};encrypt={_Encrypt};");

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
        var now = DateTime.UtcNow;

        if (entity.State == EntityState.Added)
        {
          ((BaseEntity)entity.Entity).CreatedAt = now;
        }
          ((BaseEntity)entity.Entity).UpdatedAt = now;
      }
    }

  }

}
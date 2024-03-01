using Application.Modules.LaborModule.Models;
using Application.Modules.Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Config
{
  public class ConnectionContext : DbContext
  {
    public DbSet<User> User { get; set; }
    public DbSet<Labor> Labor { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=localhost;Database=master;User Id=sa;Password=Mssql!Passw0rd;");
  }

}

using System.ComponentModel.DataAnnotations.Schema;


namespace Application.Modules.Auth.Models
{
  [Table("users")]
  public class User(int id, string name, string username, string email, string password, DateTime? createdAt, DateTime? updatedAt)
  {

    [Column("id")]
    public required int Id { get; set; } = id;

    [Column("name")]
    public required string Name { get; set; } = name;

    [Column("username")]
    public required string Username { get; set; } = username;

    [Column("email")]
    public required string Email { get; set; } = email;

    [Column("password")]
    public required string Password { get; set; } = password;

    [Column("created_at")]
    public required DateTime? CreatedAt { get; set; } = createdAt;

    [Column("updated_at")]
    public required DateTime? UpdatedAt { get; set; } = updatedAt;
  }
}
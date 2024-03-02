
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Application.Modules.Auth.Models
{
  [Table("users", Schema = "mtasks")]
  public class User(string name, string username, string email, string password)
  {

    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(100)")]
    [Required]
    public string Name { get; set; } = name;

    [Column("username", TypeName = "varchar(100)")]
    [Required]

    public string Username { get; set; } = username;

    [Column("email", TypeName = "varchar(100)")]
    [Required]

    public string Email { get; set; } = email;

    [Column("password", TypeName = "varchar(200)")]
    [Required]

    public string Password { get; set; } = password;

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
  }
}
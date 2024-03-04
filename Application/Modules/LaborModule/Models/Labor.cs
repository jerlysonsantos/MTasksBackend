
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Modules.Auth.Models;


namespace Application.Modules.LaborModule.Models
{
  [Table("labors", Schema = "mtasks")]
  public class Labor(string title, string description, bool isDone)
  {
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("title", TypeName = "varchar(100)")]
    [Required]
    public string Title { get; set; } = title;

    [Column("description", TypeName = "varchar(100)")]
    [Required]
    public string Description { get; set; } = description;

    [Column("is_done")]
    [Required]
    public bool IsDone { get; set; } = isDone;

    [Column("user_id")]
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
  }
}
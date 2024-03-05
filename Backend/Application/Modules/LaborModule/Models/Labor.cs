
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Modules.Auth.Models;
using Application.Utils.BaseEntity;


namespace Application.Modules.LaborModule.Models
{
  [Table("labors", Schema = "mtasks")]
  public class Labor : BaseEntity
  {
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("title", TypeName = "varchar(100)")]
    [Required]
    public string? Title { get; set; }

    [Column("description", TypeName = "varchar(100)")]
    [Required]
    public string? Description { get; set; }

    [Column("is_done")]
    [Required]
    public bool IsDone { get; set; }

    [Column("user_id")]
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

  }
}
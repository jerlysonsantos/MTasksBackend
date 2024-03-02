
using System.ComponentModel.DataAnnotations.Schema;
using Application.Modules.Auth.Models;


namespace Application.Modules.LaborModule.Models
{
  [Table("labors", Schema = "mtasks")]
  public class Labor(int id, string title, string description, bool isDone, DateTime? createdAt, DateTime? updatedAt)
  {

    [Column("id")]
    public required int Id { get; set; } = id;

    [Column("title")]
    public required string Title { get; set; } = title;

    [Column("description")]
    public required string Description { get; set; } = description;

    [Column("is_done")]
    public required bool IsDone { get; set; } = isDone;

    [Column("user_id")]
    public required int UserId { get; set; }
    public required User User { get; set; }

    [Column("created_at")]
    public required DateTime? CreatedAt { get; set; } = createdAt;

    [Column("updated_at")]
    public required DateTime? UpdatedAt { get; set; } = updatedAt;
  }
}
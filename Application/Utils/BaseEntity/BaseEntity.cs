using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Utils.BaseEntity
{
  public abstract class BaseEntity
  {
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
  }
}

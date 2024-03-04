using System.ComponentModel.DataAnnotations;

namespace Application.Modules.LaborModule.DTO
{
  public record UpdateLaborDTO(
    [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
    string Title,
    [StringLength(100, ErrorMessage = "O descrição deve ter no máximo 100 caracteres.")]
    string Description,

    bool IsDone);

}

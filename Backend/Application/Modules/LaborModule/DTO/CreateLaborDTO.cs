using System.ComponentModel.DataAnnotations;

namespace Application.Modules.LaborModule.DTO
{
  public record CreateLaborDTO(
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
    string Title,
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(100, ErrorMessage = "O descrição deve ter no máximo 100 caracteres.")]
    string Description,

    bool IsDone);
}

using System.ComponentModel.DataAnnotations;

namespace MTasksBackend.Application.Modules.Auth.DTO
{
  public record RegisterBodyDTO(
    [Required(ErrorMessage = "O nome é obrigatório.")]
    string Name,
    [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
    string Username,
    [Required(ErrorMessage = "O Email é obrigatório.")]
    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Email não é válido.")]
    string Email,
    [Required(ErrorMessage = "A senha é obrigatória.")]
    string Password);
}

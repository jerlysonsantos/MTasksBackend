using System.ComponentModel.DataAnnotations;

namespace MTasksBackend.Application.Modules.Auth.DTO
{
  public record LoginBodyDTO(
    [Required(ErrorMessage = "O usuário é obrigatório.")]
    string Username,

    [Required(ErrorMessage = "A senha é obrigatória.")]
    string Password);
}

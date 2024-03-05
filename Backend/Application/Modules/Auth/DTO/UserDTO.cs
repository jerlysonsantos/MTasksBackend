using System.ComponentModel.DataAnnotations;

namespace Application.Modules.Auth.DTO
{
  public record UserDTO(int Id, string Name, string Username, string Email);
}



using MTasksBackend.Application.Modules.Auth.DTO;

namespace Application.Modules.Auth.Interfaces
{
  public interface IAuthService
  {
    void Get();

    string Register(RegisterBodyDTO registerBodyDTO);

    Task<string> Login(LoginBodyDTO loginBodyDTO);
  }
}


using Application.Modules.Auth.Interfaces;
using Application.Modules.Auth.Models;

using MTasksBackend.Application.Modules.Auth.DTO;
using MTasksBackend.Application.Utils;

namespace Application.Modules.Auth.Services
{

  public class AuthService : IAuthService
  {

    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
      this._userRepository = userRepository;
    }

    public void Get()
    {
      return;
    }


    public string Register(RegisterBodyDTO registerBodyDTO)
    {
      User user = new(registerBodyDTO.Name, registerBodyDTO.Username, registerBodyDTO.Email, registerBodyDTO.Password);

      this._userRepository.Register(user);

      return "Sucesso";
    }

    public async Task<string> Login(LoginBodyDTO loginBodyDTO)
    {
      try
      {
        User user = await this._userRepository.GetOne(loginBodyDTO.Username);

        bool verified = BCrypt.Net.BCrypt.Verify(loginBodyDTO.Password, user.Password);

        if (!verified)
        {
          throw new HttpRequestException("Erro ao fazer login. Verifique suas credenciais.");
        }

        return TokenService.GenerateToken(user);
      }
      catch (Exception)
      {

        throw new HttpRequestException("Erro ao fazer login. Verifique suas credenciais.");
      }
    }

  }
}
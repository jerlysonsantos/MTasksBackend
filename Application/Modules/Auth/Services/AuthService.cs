

using Application.Modules.Auth.Interfaces;
using Application.Modules.Auth.Models;

using MTasksBackend.Application.Modules.Auth.DTO;
using MTasksBackend.Application.Utils;

namespace Application.Modules.Auth
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

    public string Login(LoginBodyDTO loginBodyDTO)
    {
      User user = this._userRepository.GetOne(loginBodyDTO.Username);

      bool verified = BCrypt.Net.BCrypt.Verify(loginBodyDTO.Password, user.Password);

      if (!verified)
      {
        throw new System.Exception("Invalid password");
      }

      return TokenService.GenerateToken(user);
    }

  }
}
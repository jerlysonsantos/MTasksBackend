using System.Security.Claims;
using Antelcat.Attributes;
using Application.Modules.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTasksBackend.Application.Modules.Auth.DTO;

namespace Application.Modules.Auth
{
  [ApiController]
  [Route("api/public/v1/auth")]
  public class AuthController : ControllerBase
  {

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      this._authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginBodyDTO loginBodyDTO)
    {
      CookieOptions cookieOptions = new()
      {
        Expires = DateTime.UtcNow.AddHours(2),
        Path = "/"
      };

      string token = await this._authService.Login(loginBodyDTO);

      Response.Cookies.Append("token", token, cookieOptions);

      return Ok("Sucesso");
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register(RegisterBodyDTO registerBodyDTO)
    {
      return Ok(this._authService.Register(registerBodyDTO));
    }
  }
}
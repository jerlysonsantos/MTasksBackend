using System.Net;
using Application.Modules.Auth.Interfaces;
using Application.Utils.ErrorHandle;
using Microsoft.AspNetCore.Mvc;
using Application.Modules.Auth.DTO;

namespace Application.Modules.Auth
{
  [ApiController]
  [Route("api/public/v1/auth")]
  public class AuthController : Controller
  {

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      this._authService = authService;
    }

    [HttpPost]
    public async Task<JsonResult> Login([FromBody] LoginBodyDTO loginBodyDTO)
    {
      try
      {
        CookieOptions cookieOptions = new()
        {
          Expires = DateTime.UtcNow.AddHours(2),
          Path = "/"
        };

        string token = await this._authService.Login(loginBodyDTO);

        Response.Cookies.Append("token", token, cookieOptions);

        return Json(new { message = "Usuário válido" });
      }
      catch (Exception ex)
      {
        throw new HttpRequestException(ex.Message);
      }
    }

    [HttpPost]
    [Route("register")]
    public JsonResult Register(RegisterBodyDTO registerBodyDTO)
    {
      try
      {
        this._authService.Register(registerBodyDTO);

        return Json(new { message = "Usuário criado com sucesso" });
      }
      catch (Exception ex)
      {
        throw new HttpRequestException(ex.Message);
      }
    }
  }
}
using Microsoft.AspNetCore.Mvc;

namespace Application.Modules.Auth
{
  [ApiController]
  [Route("api/v1/auth")]
  public class AuthController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("Ok");
    }

  }
}
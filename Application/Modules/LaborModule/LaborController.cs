using Microsoft.AspNetCore.Mvc;

namespace Application.Modules.LaborModule
{
  [ApiController]
  [Route("api/v1/labor")]
  public class LaborController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("Ok");
    }

  }
}
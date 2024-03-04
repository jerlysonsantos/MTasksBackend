using System.Net;
using Application.Modules.LaborModule.DTO;
using Application.Modules.LaborModule.Interfaces;
using Application.Utils.ErrorHandle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Modules.LaborModule
{
  [ApiController]
  [Route("api/v1/labor")]
  public class LaborController(ILaborService laborService) : Controller
  {

    private readonly ILaborService _laborService = laborService;

    [HttpGet]
    [Authorize]
    public async Task<JsonResult> GetAll()
    {
      int userIndentityId = int.Parse(User.Identity?.Name ?? "0");

      List<LaborDTO> labors = await this._laborService.Get(userIndentityId);

      return Json(new { labors });
    }
    [HttpGet("{id}")]
    [Authorize]
    public async Task<JsonResult> GetById(int id)
    {
      try
      {
        int userIndentityId = int.Parse(User.Identity?.Name ?? "0");

        LaborDTO labor = await this._laborService.GetById(id, userIndentityId);

        return Json(new { labor });
      }
      catch (Exception ex)
      {
        return Json(new ErrorDetails()
        {
          StatusCode = (int)HttpStatusCode.NotFound,
          Message = ex.Message
        }
       );

      }
    }

    [HttpPost]
    [Authorize]
    public async Task<JsonResult> Create(CreateLaborDTO labor)
    {
      int userIndentityId = int.Parse(User.Identity?.Name ?? "0");

      LaborDTO laborAdded = await this._laborService.Add(labor, userIndentityId);

      return Json(new { message = "Tarefa Adicionada", labor = laborAdded });

    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<JsonResult> Update(int id, UpdateLaborDTO labor)
    {
      int userIndentityId = int.Parse(User.Identity?.Name ?? "0");

      LaborDTO laborUpdated = await this._laborService.Update(id, labor, userIndentityId);

      return Json(new { message = "Tarefa Atualizada", labor = laborUpdated });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public JsonResult Delete(int id)
    {
      int userIndentityId = int.Parse(User.Identity?.Name ?? "0");

      this._laborService.Delete(id, userIndentityId);

      return Json(new { message = "Tarefa removida" });
    }
  }


}

using System.Net;
using Application.Modules.LaborModule.DTO;
using Application.Modules.LaborModule.Interfaces;
using Application.Utils.ErrorHandle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Utils;

namespace Application.Modules.LaborModule
{
  [ApiController]
  [Route("api/v1/labor")]
  public class LaborController(ILaborService laborService) : Controller
  {

    private readonly ILaborService _laborService = laborService;

    [HttpGet]
    [Authorize]
    public async Task<JsonResult> GetAll(int page = 0, int size = 10)
    {
      try
      {
        int userIndentityId = TokenService.GetUserId(User);

        LaborPaginateDTO laborsPaginateDto = await this._laborService.Get(userIndentityId, page, size);

        return Json(new { labors = laborsPaginateDto.Labors, size = laborsPaginateDto.Size });
      }

      catch (Exception ex)
      {
        return Json(new ErrorDetails()
        {
          StatusCode = (int)HttpStatusCode.BadRequest,
          Message = ex.Message
        }
       );
      }
    }
    [HttpGet("{id}")]
    [Authorize]
    public async Task<JsonResult> GetById(int id)
    {
      try
      {
        int userIndentityId = TokenService.GetUserId(User);

        LaborDTO labor = await this._laborService.GetById(id, userIndentityId);

        return Json(new { labor });
      }
      catch (Exception ex)
      {
        return Json(new ErrorDetails()
        {
          StatusCode = (int)HttpStatusCode.BadRequest,
          Message = ex.Message
        }
       );
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<JsonResult> Create(CreateLaborDTO labor)
    {
      try
      {
        int userIndentityId = TokenService.GetUserId(User);

        LaborDTO laborAdded = await this._laborService.Add(labor, userIndentityId);

        return Json(new { message = "Tarefa Adicionada", labor = laborAdded });
      }
      catch (Exception ex)
      {
        return Json(new ErrorDetails()
        {
          StatusCode = (int)HttpStatusCode.BadRequest,
          Message = ex.Message
        }
       );
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<JsonResult> Update(int id, UpdateLaborDTO labor)
    {
      try
      {
        int userIndentityId = TokenService.GetUserId(User);

        LaborDTO laborUpdated = await this._laborService.Update(id, labor, userIndentityId);

        return Json(new { message = "Tarefa Atualizada", labor = laborUpdated });
      }
      catch (Exception ex)
      {
        return Json(new ErrorDetails()
        {
          StatusCode = (int)HttpStatusCode.BadRequest,
          Message = ex.Message
        }
       );
      }
    }

    [HttpPatch("{id}/done")]
    [Authorize]
    public async Task<JsonResult> Done(int id)
    {
      try
      {
        int userIndentityId = TokenService.GetUserId(User);

        LaborDTO laborUpdated = await this._laborService.Done(id, userIndentityId);

        return Json(new { message = "Tarefa Atualizada", labor = laborUpdated });
      }
      catch (Exception ex)
      {
        return Json(new ErrorDetails()
        {
          StatusCode = (int)HttpStatusCode.BadRequest,
          Message = ex.Message
        }
       );
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<JsonResult> Delete(int id)
    {
      try
      {
        int userIndentityId = TokenService.GetUserId(User);

        await this._laborService.Delete(id, userIndentityId);

        return Json(new { message = "Tarefa removida" });
      }
      catch (Exception ex)
      {
        return Json(new ErrorDetails()
        {
          StatusCode = (int)HttpStatusCode.BadRequest,
          Message = ex.Message
        }
       );
      }
    }
  }
}

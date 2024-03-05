
using Application.Modules.LaborModule.DTO;
namespace Application.Modules.LaborModule.Interfaces
{
  public interface ILaborService
  {
    Task<LaborPaginateDTO> Get(int userId, int page, int size);
    Task<LaborDTO> GetById(int id, int userId);
    Task<LaborDTO> Add(CreateLaborDTO labor, int userId);
    Task<LaborDTO> Update(int id, UpdateLaborDTO labor, int userId);
    Task<LaborDTO> Done(int id, int userId);
    Task<LaborDTO> Delete(int id, int userId);
  }
}

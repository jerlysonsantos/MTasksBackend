
using Application.Modules.LaborModule.DTO;
using Application.Modules.LaborModule.Models;

namespace Application.Modules.LaborModule.Interfaces
{
  public interface ILaborService
  {
    Task<List<LaborDTO>> Get(int userId);
    Task<LaborDTO> GetById(int id, int userId);
    Task<LaborDTO> Add(CreateLaborDTO labor, int userId);
    Task<LaborDTO> Update(int id, UpdateLaborDTO labor, int userId);
    void Delete(int id, int userId);
  }
}

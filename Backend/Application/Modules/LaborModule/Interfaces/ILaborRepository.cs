using Application.Modules.LaborModule.DTO;
using Application.Modules.LaborModule.Models;

namespace Application.Modules.LaborModule.Interfaces
{
  public interface ILaborRepository
  {
    Task<Labor> GetOne(int id, int userId);
    Task<List<Labor>> GetAll(int userId, int page = 0, int size = 10);
    Task<int> GetCount(int userId, int page = 0, int size = 10);
    Task<Labor> Add(Labor labor);
    Task<Labor> Update(int id, UpdateLaborDTO labor, int userId);
    Task<Labor> Delete(int id, int userId);

    Task<Labor> Done(int id, int userId);

  }
}
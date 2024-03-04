using Application.Modules.LaborModule.Models;

namespace Application.Modules.LaborModule.Interfaces
{
  public interface ILaborRepository
  {
    Task<Labor> GetOne(int id, int userId);
    Task<List<Labor>> GetAll(int userId);
    Task<Labor> Add(Labor labor);
    Task<Labor> Update(Labor labor);
    void Delete(int id, int userId);

  }
}
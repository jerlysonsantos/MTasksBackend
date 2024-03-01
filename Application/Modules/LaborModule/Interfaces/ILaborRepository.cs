using Application.Modules.LaborModule.Models;

namespace Application.Modules.LaborModule.Interfaces
{
  public interface ILaborRepository
  {
    Labor GetOne(int id);
  }
}
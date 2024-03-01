
using Application.Config;
using Application.Modules.LaborModule.Interfaces;
using Application.Modules.LaborModule.Models;

namespace Application.Modules.LaborModule.Repositories
{
  public class LaborRepository : ILaborRepository
  {

    private readonly ConnectionContext _context = new();


    public Labor GetOne(int id)
    {
      Labor user = this._context.Labor.Where(User => User.Id == id).First();

      return user;
    }


  }
}
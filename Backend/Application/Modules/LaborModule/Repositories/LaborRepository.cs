using System.Net;
using Application.Config;
using Application.Modules.LaborModule.Interfaces;
using Application.Modules.LaborModule.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Modules.LaborModule.Repositories
{
  public class LaborRepository : ILaborRepository
  {
    private readonly ConnectionContext _context = new();

    public async Task<Labor> Add(Labor labor)
    {
      _context.Labor.Add(labor);
      await _context.SaveChangesAsync();
      return labor;
    }

    public async Task<Labor> GetOne(int id, int userId)
    {
      try
      {
        Labor labor = await _context.Labor
          .Where(labor => labor.UserId == userId)
          .Where(labor => labor.Id == id).FirstAsync();

        return labor;
      }
      catch (InvalidOperationException)
      {
        throw new InvalidOperationException("Tarefa não encontrada");
      }

    }

    public async Task<List<Labor>> GetAll(int userId, int page = 0, int size = 10)
    {
      int count = await _context.Labor.Where(labor => labor.UserId == userId).CountAsync();
      return await _context.Labor.Skip(page * size).Take(size).OrderBy(labor => labor.CreatedAt).Where(labor => labor.UserId == userId).ToListAsync();
    }

    public async Task<int> GetCount(int userId, int page = 0, int size = 10)
    {
      return await _context.Labor.Where(labor => labor.UserId == userId).CountAsync();
    }

    public async Task<Labor> Update(Labor labor)
    {
      var getLabor = _context.Labor.Where(labor => labor.Id == labor.Id).First();

      labor.CreatedAt = getLabor.CreatedAt;

      _context.Entry(labor).State = EntityState.Modified;
      await _context.SaveChangesAsync();
      return labor;
    }

    public async Task<Labor> Delete(int id, int userId)
    {
      try
      {
        var labor = await _context.Labor
          .Where(labor => labor.UserId == userId)
          .Where(labor => labor.Id == id).FirstAsync();

        if (labor != null)
        {
          _context.Labor.Remove(labor);
          await _context.SaveChangesAsync();
        }

        return labor;
      }
      catch (InvalidOperationException)
      {
        throw new InvalidOperationException("Tarefa não encontrada");
      }
    }
  }
}





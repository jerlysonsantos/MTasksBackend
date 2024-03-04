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

    public async Task<List<Labor>> GetAll(int userId)
    {
      return await _context.Labor.Where(labor => labor.UserId == userId).ToListAsync();
    }

    public async Task<Labor> Update(Labor labor)
    {
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





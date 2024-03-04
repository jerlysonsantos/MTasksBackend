
using Mapster;

using Application.Modules.LaborModule.DTO;
using Application.Modules.LaborModule.Interfaces;
using Application.Modules.LaborModule.Models;
namespace Application.Modules.LaborModule.Services
{

  public class LaborService(ILaborRepository laborRepository) : ILaborService
  {

    private readonly ILaborRepository _laborRepository = laborRepository;

    public async Task<LaborPaginateDTO> Get(int userId, int page, int size)
    {

      int count = await _laborRepository.GetCount(userId, page, size);
      List<Labor> labors = await _laborRepository.GetAll(userId, page, size);

      List<LaborDTO> laborsDto = labors.Adapt<List<LaborDTO>>();

      return new LaborPaginateDTO(count, laborsDto);
    }

    public async Task<LaborDTO> GetById(int id, int userId)
    {

      Labor labor = await _laborRepository.GetOne(id, userId);
      return labor.Adapt<LaborDTO>();
    }

    public async Task<LaborDTO> Add(CreateLaborDTO laborDTO, int userId)
    {

      Labor newLabor = new()
      {
        Title = laborDTO.Title,
        Description = laborDTO.Description,
        IsDone = laborDTO.IsDone,
        UserId = userId
      };

      Labor labor = await this._laborRepository.Add(newLabor);

      return labor.Adapt<LaborDTO>();
    }

    public async Task<LaborDTO> Update(int id, UpdateLaborDTO laborDTO, int userId)
    {
      Labor updateLabor = new()
      {
        Id = id,
        Title = laborDTO.Title,
        Description = laborDTO.Description,
        IsDone = laborDTO.IsDone,
        UserId = userId
      };

      Labor labor = await this._laborRepository.Update(updateLabor);

      return labor.Adapt<LaborDTO>();
    }

    public async Task<LaborDTO> Done(int id, int userId)
    {
      try
      {
        Labor labor = await this._laborRepository.GetOne(id, userId);

        labor.IsDone = true;

        Labor laborUpdated = await this._laborRepository.Update(labor);

        return laborUpdated.Adapt<LaborDTO>();
      }
      catch (Exception)
      {

        throw new Exception("Erro ao salvar tarefa");
      }

    }

    public async Task<LaborDTO> Delete(int id, int userId)
    {
      try
      {
        Labor laborRemoved = await this._laborRepository.Delete(id, userId);
        return laborRemoved.Adapt<LaborDTO>();
      }
      catch (Exception)
      {
        throw new Exception("Erro ao remover tarefa");
      }
    }
  }
}
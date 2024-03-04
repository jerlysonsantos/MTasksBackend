
using Mapster;

using Application.Modules.LaborModule.DTO;
using Application.Modules.LaborModule.Interfaces;
using Application.Modules.LaborModule.Models;
namespace Application.Modules.LaborModule.Services
{

  public class LaborService(ILaborRepository laborRepository) : ILaborService
  {

    private readonly ILaborRepository _laborRepository = laborRepository;

    public async Task<List<LaborDTO>> Get(int userId)
    {

      List<Labor> labors = await _laborRepository.GetAll(userId);

      return labors.Adapt<List<LaborDTO>>();
    }

    public async Task<LaborDTO> GetById(int id, int userId)
    {

      Labor labor = await _laborRepository.GetOne(id, userId);
      return labor.Adapt<LaborDTO>();
    }

    public async Task<LaborDTO> Add(CreateLaborDTO laborDTO, int userId)
    {

      Labor newLabor = new(laborDTO.Title, laborDTO.Description, laborDTO.IsDone)
      {
        UserId = userId
      };

      Labor labor = await this._laborRepository.Add(newLabor);

      return labor.Adapt<LaborDTO>();
    }

    public async Task<LaborDTO> Update(int id, UpdateLaborDTO laborDTO, int userId)
    {
      Labor newLabor = new(laborDTO.Title, laborDTO.Description, laborDTO.IsDone)
      {
        Id = id,
        UserId = userId
      };

      Labor labor = await this._laborRepository.Update(newLabor);

      return labor.Adapt<LaborDTO>();
    }

    public void Delete(int id, int userId)
    {
      this._laborRepository.Delete(id, userId);
    }
  }
}
using Application.Modules.Auth.Models;

namespace Application.Modules.Auth.Interfaces
{
  public interface IUserRepository
  {
    Task<User> GetOne(string username);

    void Register(User user);
  }
}
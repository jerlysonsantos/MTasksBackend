using Application.Modules.Auth.Models;

namespace Application.Modules.Auth.Interfaces
{
  public interface IUserRepository
  {
    User GetOne(string username);

    void Register(User user);
  }
}

using Application.Config;
using Application.Modules.Auth.Interfaces;
using Application.Modules.Auth.Models;

namespace Application.Modules.Auth.Repositories
{
  public class UserRepository : IUserRepository
  {

    private readonly ConnectionContext _context = new();


    public User GetOne(string username)
    {
      User user = this._context.User.Where(User => User.Username == username).First();

      return user;
    }


  }
}
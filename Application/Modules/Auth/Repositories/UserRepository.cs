
using Application.Config;
using Application.Modules.Auth.Interfaces;
using Application.Modules.Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Modules.Auth.Repositories
{
  public class UserRepository : IUserRepository
  {

    private readonly ConnectionContext _context = new();


    public async Task<User> GetOne(string username)
    {
      User user = await this._context.User.Where(User => User.Username == username).FirstAsync();

      return user;
    }

    public async void Register(User user)
    {

      user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

      this._context.User.Add(user);
      await this._context.SaveChangesAsync();
    }

  }
}
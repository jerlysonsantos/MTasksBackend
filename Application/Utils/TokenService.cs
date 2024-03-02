using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Config;
using Application.Modules.Auth.Models;
using Microsoft.IdentityModel.Tokens;

namespace MTasksBackend.Application.Utils
{
  public static class TokenService
  {
    public static string GenerateToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(JwtKey.Secret);

      var tokenConfig = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim("userId", user.Id.ToString()),
          new Claim("userName", user.Name.ToString()),
        }),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenConfig);
      return tokenHandler.WriteToken(token);
    }
  }
}


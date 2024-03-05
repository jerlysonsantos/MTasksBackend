using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Config;
using Application.Modules.Auth.Models;
using Microsoft.IdentityModel.Tokens;

namespace Application.Utils
{
  public static class TokenService
  {
    public static int GetUserId(ClaimsPrincipal user)
    {
      return int.Parse(user.Identity?.Name ?? "0");
    }

    public static string GenerateToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(JwtKey.Secret);

      var tokenConfig = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(
        [
          new Claim(ClaimTypes.Name, user.Id.ToString())
        ]),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenConfig);
      return tokenHandler.WriteToken(token);
    }
  }
}


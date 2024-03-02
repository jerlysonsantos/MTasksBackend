using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

using Application.Modules.Auth;
using Application.Modules.Auth.Interfaces;
using Application.Modules.Auth.Repositories;
using Application.Modules.LaborModule.Interfaces;
using Application.Modules.LaborModule.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Config;
using Microsoft.OpenApi.Models;

public class Startup
{
  public Startup(IConfigurationRoot configuration)
  {
    Configuration = configuration;
  }
  public IConfigurationRoot Configuration { get; }
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<ILaborRepository, LaborRepository>();
    services.AddTransient<IAuthService, AuthService>();

    var key = Encoding.ASCII.GetBytes(JwtKey.Secret);

    services.AddAuthentication(configureOptions =>
      {
        configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
        };
      });
  }
  public void Configure(WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }


    app.Use(async (context, next) =>
    {
      var token = context.Request.Cookies["token"];

      if (!string.IsNullOrEmpty(token))
      {
        context.Request.Headers.Append("Authorization", "Bearer " + token);
      }
      await next();
    });

    app.UseAuthentication();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");

    app.UseHttpsRedirection();
  }


}
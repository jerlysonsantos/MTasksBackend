using Microsoft.AspNetCore.Authentication.JwtBearer;

using Application.Modules.Auth.Services;
using Application.Modules.Auth.Interfaces;
using Application.Modules.Auth.Repositories;

using Application.Modules.LaborModule.Services;
using Application.Modules.LaborModule.Interfaces;
using Application.Modules.LaborModule.Repositories;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Config;
using Application.Utils.LoggerManager;
using Application.Utils.ErrorHandle;

public class Startup
{
  public Startup(IConfigurationRoot configuration)
  {
    Configuration = configuration;
  }
  public IConfigurationRoot Configuration { get; }
  public void ConfigureServices(IServiceCollection services)
  {

    services.AddCors();

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IAuthService, AuthService>();
    services.AddTransient<ILaborRepository, LaborRepository>();
    services.AddTransient<ILaborService, LaborService>();

    services.AddSingleton<ILoggerManager, LoggerManager>();

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
        options.Events = new JwtBearerEvents
        {
          OnChallenge = async (context) =>
          {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(new ErrorDetails()
            {
              StatusCode = context.Response.StatusCode,
              Message = "Token inválido."
            }.ToString());


            context.HandleResponse();

          }
        };
      });
  }
  public void Configure(WebApplication app)
  {

    app.UseCors(builder => builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());

    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseMyErrorHandler();

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
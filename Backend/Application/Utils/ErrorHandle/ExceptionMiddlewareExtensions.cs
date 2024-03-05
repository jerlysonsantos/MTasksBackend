using System.Net;
using Microsoft.AspNetCore.Diagnostics;

using Application.Utils.ErrorHandle;
using Application.Utils.LoggerManager;

public static class ExceptionMiddlewareExtensions
{
  public static IApplicationBuilder UseMyErrorHandler(this IApplicationBuilder appBuilder)
  {
    return appBuilder.UseMiddleware<ErrorHandler>();
  }

}
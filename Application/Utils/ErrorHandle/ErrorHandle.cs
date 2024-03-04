using System.Net;
using System.Text.Json.Serialization;
using Application.Utils.ErrorHandle;

public class ErrorHandler
{
  private readonly RequestDelegate _next;
  private readonly ILogger _log;

  public ErrorHandler(RequestDelegate next, ILoggerFactory log)
  {
    this._next = next;
    this._log = log.CreateLogger("MyErrorHandler");
  }

  public async Task Invoke(HttpContext httpContext)
  {
    try
    {
      await _next(httpContext);
    }
    catch (Exception ex)
    {
      await HandleErrorAsync(httpContext, ex);
    }
  }

  private async Task HandleErrorAsync(HttpContext context, Exception exception)
  {
    var errorResponse = new ErrorDetails()
    {
      StatusCode = (int)HttpStatusCode.InternalServerError,
      Message = "Erro interno"
    };

    _log.LogError($"Error: {exception.Message}");
    _log.LogError($"Stack: {exception.StackTrace}");

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)errorResponse.StatusCode;
    await context.Response.WriteAsync(errorResponse.ToString());
  }
}

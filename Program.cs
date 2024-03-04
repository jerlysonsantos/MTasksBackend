using NLog;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

LogManager.Setup();

var app = builder.Build();
startup.Configure(app);
app.Run();
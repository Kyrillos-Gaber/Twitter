using Twitter.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices();

app.ConfigurePipeline();

app.Run();

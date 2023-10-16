using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Twitter.Application;
using Twitter.Application.Services.Implementation;
using Twitter.Infrastructure;

namespace Twitter.Api;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        builder.Services.AddAuthentication();

        builder.Services.ConfigureJWT(builder.Configuration);

        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed((hosts) => true));
        });
        builder.Services.AddHttpContextAccessor();
        // Configure swagger to accept tokens
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApi", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }});
            });
        }

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        var fileProvider = Path.Combine(app.Environment.ContentRootPath, "Uploads");
        if (!Directory.Exists(fileProvider))
            Directory.CreateDirectory(fileProvider);
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(fileProvider),
            RequestPath = "/resources"
        });

        app.UseHttpsRedirection();
        //app.ConfigureServicesPipeline();
        app.UseCors("AllowAll");
        app.UseAuthorization();
        app.UseAuthorization();

        app.MapControllers();
        app.MapHub<ChatHub>("hubs/chat");


        return app;
    }
}

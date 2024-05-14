
using CapitalPlacementTask1.Data;
using CapitalPlacementTask1.ExceptionHandler;
using CapitalPlacementTask1.Models.Entities;
using CapitalPlacementTask1.Services;
using CapitalPlacementTask1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CapitalPlacementTask1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>();

            // Add services to the container.
            builder.Services.AddScoped<IProgramService, ProgramService>();
            builder.Services.AddScoped<IRepositoryService<ProgramEntity>, RepositoryService<ProgramEntity>>();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddScoped<IRepositoryService<ApplicationEntity>, RepositoryService<ApplicationEntity>>();


            builder.Services.AddControllers()
                    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
                c.EnableAnnotations(); 
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services?.GetService<IServiceScopeFactory>()?.CreateScope();
                var context = scope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var result = context!.Database.EnsureCreated();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

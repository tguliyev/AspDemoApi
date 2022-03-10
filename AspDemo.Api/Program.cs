using AspDemo.Api.EntityFramework;
using AspDemo.Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;

namespace AspDemo.Api
{
    public class Program
    {
        static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDataContext, DataContext>();
            builder.Services.AddSingleton<IItemsRepository, EFCoreItemsRepository>();
            
            builder.Services.AddHealthChecks()
                            .AddSqlServer(connectionString: $"Data Source={builder.Configuration["mssql:data-source"]};" + 
                                                            $"Database={builder.Configuration["mssql:database"]};" + 
                                                            $"User Id={builder.Configuration["mssql:username"]};"+
                                                            $"Password={builder.Configuration["mssql:password"]}", 
                                          name: "mssql server",
                                          timeout: TimeSpan.FromSeconds(3), 
                                          tags: new string[] {"ready"});
            

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.MapHealthChecks("/health/ready", new HealthCheckOptions()
            {
                Predicate = check => check.Tags.Contains("ready"),
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonSerializer.Serialize(new 
                    {
                        status = report.Status.ToString(),
                        cheks = report.Entries.Select(entry => new
                        {
                            name = entry.Key,
                            status = entry.Value.Status.ToString(),
                            exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                            duration = entry.Value.Duration.ToString()
                        })
                    });

                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });

            app.MapHealthChecks("/health/live", new HealthCheckOptions()
            {
                Predicate = _ => false
            });

            if (builder.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            
        }
    }
}
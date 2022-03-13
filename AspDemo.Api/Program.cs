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
            WebApplicationBuilder Builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            Builder.Services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Builder.Services.AddEndpointsApiExplorer();
            Builder.Services.AddSwaggerGen();

            Builder.Services.AddSingleton<IDataContext, DataContext>();
            Builder.Services.AddSingleton<IItemsRepository, EFCoreItemsRepository>();
            
            Builder.Services.AddHealthChecks()
                            .AddSqlServer(connectionString: $"Data Source={Builder.Configuration["mssql:data-source"]};" + 
                                                            $"Database={Builder.Configuration["mssql:database"]};" + 
                                                            $"User Id={Builder.Configuration["mssql:username"]};"+
                                                            $"Password={Builder.Configuration["mssql:password"]}", 
                                          name: "mssql server",
                                          timeout: TimeSpan.FromSeconds(3), 
                                          tags: new string[] {"ready"});
            

            WebApplication App = Builder.Build();

            // Configure the HTTP request pipeline.
            if (App.Environment.IsDevelopment())
            {
                App.UseSwagger();
                App.UseSwaggerUI();
            }
            
            App.MapHealthChecks("/health/ready", new HealthCheckOptions()
            {
                Predicate = check => check.Tags.Contains("ready"),
                ResponseWriter = async (context, report) =>
                {
                    var Result = JsonSerializer.Serialize(new 
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
                    await context.Response.WriteAsync(Result);
                }
            });

            App.MapHealthChecks("/health/live", new HealthCheckOptions()
            {
                Predicate = _ => false
            });

            if (Builder.Environment.IsDevelopment())
            {
                App.UseHttpsRedirection();
            }

            App.UseAuthorization();

            App.MapControllers();

            App.Run();

            
        }
    }
}
using System;
using AspDemo.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AspDemo.Api.EntityFramework
{
    public class DataContext : DbContext, IDataContext
    {
        private IConfiguration config;
        private readonly string DB_PASSWORD;
        private readonly string DB_SOURCE;
        private readonly string DB_DATABASE;
        private readonly string DB_USERNAME;
        private string CONNECTION_STRING 
        { 
            get => $"Data Source={DB_SOURCE};Database={DB_DATABASE};User Id={DB_USERNAME};Password={DB_PASSWORD}"; 
        }
        public DbSet<Item>? Items { get; set; }

        public DataContext(IConfiguration _config)
        {
            this.config = _config;
            DB_PASSWORD = config["mssql:password"];
            DB_SOURCE = config["mssql:data-source"];
            DB_DATABASE = config["mssql:database"];
            DB_USERNAME = config["mssql:username"];
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
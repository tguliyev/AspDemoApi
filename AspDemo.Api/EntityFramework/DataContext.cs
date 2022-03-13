using System;
using AspDemo.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AspDemo.Api.EntityFramework
{
    public class DataContext : DbContext, IDataContext
    {
        private IConfiguration Config;
        private readonly string DB_PASSWORD;
        private readonly string DB_SOURCE;
        private readonly string DB_DATABASE;
        private readonly string DB_USERNAME;
        private string CONNECTION_STRING 
        { 
            get => $"Data Source={DB_SOURCE};Database={DB_DATABASE};User Id={DB_USERNAME};Password={DB_PASSWORD}"; 
        }
        public DbSet<Item>? Items { get; set; }

        public DataContext(IConfiguration Config)
        {
            this.Config = Config;
            DB_PASSWORD = Config["mssql:password"];
            DB_SOURCE = Config["mssql:data-source"];
            DB_DATABASE = Config["mssql:database"];
            DB_USERNAME = Config["mssql:username"];
        }
        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseSqlServer(CONNECTION_STRING);
            base.OnConfiguring(OptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);
        }
    }
}
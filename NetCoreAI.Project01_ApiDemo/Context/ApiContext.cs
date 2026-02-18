using Microsoft.EntityFrameworkCore;
using NetCoreAI.Project1_ApiDemo.Entities;
using System.Collections.Generic;

namespace NetCoreAI.Project01_ApiDemo.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-I05VV5A;initial catalog=ApiAIDb;integrated security=true;trustservercertificate=true");
        }
        public DbSet<Customer> Customers { get; set; }
    }
}

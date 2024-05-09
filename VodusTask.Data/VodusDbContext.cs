using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodusTask.Data
{
    public class VodusDbContext : DbContext
    {
        public VodusDbContext(DbContextOptions<VodusDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=1234sadi;Host=localhost;Port=5432;Database=Vodus;");
        }
        public DbSet<Order> Order { get; set; }
        public DbSet<ColumnConfigurator> ColumnConfigurator { get; set; }
    }
}

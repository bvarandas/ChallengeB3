using ChallengeB3.Domain.Models;
using ChallengeB3.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChallengeB3.Infra.Data.Context
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration _config;
        public DbSet<Register> Registers { get; set; }
        public DbContextClass(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegisterMap());
            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_config.GetConnectionString("Default"));
            //base.OnConfiguring(optionsBuilder);
        }
    }
}

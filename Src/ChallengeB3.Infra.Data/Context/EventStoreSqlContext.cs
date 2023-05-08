using ChallengeB3.Domain.Events;
using ChallengeB3.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ChallengeB3.Infra.Data.Context;

public class EventStoreSqlContext : DbContext
{
    public DbSet<StoredEvent> StoredEvent { get; set; } = null!;
    private readonly IHostingEnvironment _env;
    public EventStoreSqlContext (IHostingEnvironment env)
    {
        _env = env;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StoredEventMap());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

    }
}

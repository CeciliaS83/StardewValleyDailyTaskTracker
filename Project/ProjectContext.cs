using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

public class ProjectContext : DbContext
{
    [NotMapped]
    public DbSet<StardewValley> Task { get; private set; }
    public DbSet<Task> Tasks { get; set; }

    public ProjectContext()
    {
    }

    public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (options.IsConfigured)
        {
            return;
        }

        IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets<ProjectContext>()
            .Build();

        options.UseSqlServer(config["ConnectionStrings:Project"]);
    }
}
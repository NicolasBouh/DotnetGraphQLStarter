using DotnetGraphQLStarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQLStarter.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;

    public DbSet<Training> Trainings { get; set; } = default!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
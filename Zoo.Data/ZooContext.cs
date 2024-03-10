using Microsoft.EntityFrameworkCore;
using Zoo.Data.Models;

namespace Zoo.Data;

public class ZooContext : DbContext
{
    public ZooContext(DbContextOptions<ZooContext> options) 
        : base(options)
        {
             
        }

    public DbSet<AnimalClass> AnimalClasses => Set<AnimalClass>();

    public DbSet<Animal> Animals => Set<Animal>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=zoo-postgres;Server=zoo-postgres;Port=5432;Database=zoo;Username=postgres;Password=postgres");
        
            // Configuration.GetConnectionString("ZooContext")
}
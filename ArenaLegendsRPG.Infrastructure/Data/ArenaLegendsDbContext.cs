using ArenaLegendsRPG.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArenaLegendsRPG.Infrastructure.Data;

public class ArenaLegendsDbContext : DbContext
{
    public ArenaLegendsDbContext(DbContextOptions<ArenaLegendsDbContext> options) : base(options) { }
    public DbSet<CharacterEntity> Characters => Set<CharacterEntity>();
    public DbSet<ItemEntity> Items => Set<ItemEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WeaponEntity>();
        modelBuilder.Entity<ProtectionEntity>();
    }
}


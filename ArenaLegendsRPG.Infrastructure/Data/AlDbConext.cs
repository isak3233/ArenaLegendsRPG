using Microsoft.EntityFrameworkCore;

namespace ArenaLegendsRPG.Infrastructure.Data;

public class AlDbConext : DbContext
{
    public AlDbConext(DbContextOptions<AlDbConext> options) : base(options) { }
}
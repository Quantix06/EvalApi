using EvalApi.Src.Core.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvalApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PostEntity> Posts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}

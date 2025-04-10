using Domain.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> optionsBuilder)
    : Microsoft.EntityFrameworkCore.DbContext(optionsBuilder)
{
    public DbSet<Client> Clients { get; set; }
}
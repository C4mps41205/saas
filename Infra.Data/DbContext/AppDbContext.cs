using Domain.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> optionsBuilder)
    : Microsoft.EntityFrameworkCore.DbContext(optionsBuilder)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Client

        modelBuilder
            .Entity<Client>()
            .HasOne(c => c.BelongsTo)
            .WithMany()
            .HasForeignKey(c => c.BelongsToId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region Employee

        modelBuilder
            .Entity<Employee>()
            .HasMany(e => e.Cards)
            .WithOne(c => c.Employee);

        #endregion

        #region Card

        modelBuilder
            .Entity<Card>()
            .HasMany(c => c.Clients)
            .WithMany();
        
        modelBuilder
            .Entity<Card>()
            .HasOne<Employee>(c => c.Employee)
            .WithMany();

        #endregion
    }
}
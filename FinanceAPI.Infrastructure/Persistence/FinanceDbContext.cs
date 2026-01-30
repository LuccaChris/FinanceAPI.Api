using FinanceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace FinanceAPI.Infrastructure.Persistence;

public class FinanceDbContext : DbContext
{
    public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options)
    {
    }
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users"); 

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Username)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(x => x.Email)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.Property(x => x.PasswordHash)
                  .IsRequired()
                  .HasMaxLength(500);

            entity.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Accounts"); 

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(x => x.Type)
                  .IsRequired();

            entity.Property(x => x.Balance)
                  .IsRequired()
                  .HasColumnType("decimal(18,2)");

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    
    
}
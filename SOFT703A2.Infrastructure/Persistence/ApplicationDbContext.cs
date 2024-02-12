using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SOFT703A2.Domain.Models;

namespace SOFT703A2.Infrastructure.Persistence;

public class ApplicationDbContext: IdentityDbContext<User, Role, string>
{

    public DbSet<Product> Product { get; set; }
    public DbSet<Trolley> Trolley { get; set; }
    public DbSet<ProductXTrolley> ProductXTrolley { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Login> Login { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>()
            .HasOne(l => l.User)
            .WithMany(u => u.Logins)
            .HasForeignKey(l => l.UserId)
            .IsRequired();
        
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired();
        modelBuilder.Entity<ProductXTrolley>()
            .HasKey(pt => new { pt.ProductId, pt.TrolleyId });

        modelBuilder.Entity<ProductXTrolley>()
            .HasOne(pt => pt.Product)
            .WithMany(p => p.ProductXTrolleys)
            .HasForeignKey(pt => pt.ProductId);

        modelBuilder.Entity<ProductXTrolley>()
            .HasOne(pt => pt.Trolley)
            .WithMany(t => t.ProductXTrolleys)
            .HasForeignKey(pt => pt.TrolleyId);
        modelBuilder.Entity<Trolley>()
            .HasOne(t => t.User)
            .WithMany(u => u.Trolleys)
            .HasForeignKey(t => t.UserId)
            .IsRequired();
        UpdateKeysConfig(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
    protected void UpdateKeysConfig(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType.GetProperties();
            foreach (var property in properties)
            {
                if (property.ClrType == typeof(string) && property.Name == "Id")
                {
                    property.ValueGenerated = ValueGenerated.OnAdd;
                }
            }
        }
    }
}
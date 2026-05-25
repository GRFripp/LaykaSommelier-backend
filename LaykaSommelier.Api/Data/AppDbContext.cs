using Microsoft.EntityFrameworkCore;
using LaykaSommelier.Api.Models;

namespace LaykaSommelier.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Drink> Drinks => Set<Drink>();
    public DbSet<Cocktail> Cocktails => Set<Cocktail>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<CocktailIngredient> CocktailsIngredients => Set<CocktailIngredient>();
    public DbSet<MakingMethod> MakingMethods => Set<MakingMethod>();
    public DbSet<DescriptorCategory> DescriptorCategories => Set<DescriptorCategory>();
    public DbSet<Descriptor> Descriptors => Set<Descriptor>();
    public DbSet<IngredientDescriptor> IngredientsDescriptors => Set<IngredientDescriptor>();
    public DbSet<Source> Sources => Set<Source>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<DescriptorReview> DescriptorsReviews => Set<DescriptorReview>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Suggestion> Suggestions => Set<Suggestion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Составные ключи
        modelBuilder.Entity<CocktailIngredient>()
            .HasKey(ci => new { ci.CocktailId, ci.IngredientId });

        modelBuilder.Entity<IngredientDescriptor>()
            .HasKey(id => new { id.IngredientId, id.DescriptorId });

        modelBuilder.Entity<DescriptorReview>()
            .HasKey(dr => new { dr.DescriptorId, dr.ReviewId });

        // Настройка связей (опционально, если хотим каскадное удаление и т.д.)
        // Пока оставим без каскадного удаления для безопасности, или можно добавить
        modelBuilder.Entity<Cocktail>()
            .HasOne(c => c.MakingMethod)
            .WithMany()
            .HasForeignKey(c => c.MakingMethodId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CocktailIngredient>()
            .HasOne(ci => ci.Cocktail)
            .WithMany()
            .HasForeignKey(ci => ci.CocktailId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CocktailIngredient>()
            .HasOne(ci => ci.Ingredient)
            .WithMany()
            .HasForeignKey(ci => ci.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Descriptor>()
            .HasOne(d => d.Category)
            .WithMany()
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<IngredientDescriptor>()
            .HasOne(id => id.Ingredient)
            .WithMany()
            .HasForeignKey(id => id.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<IngredientDescriptor>()
            .HasOne(id => id.Descriptor)
            .WithMany()
            .HasForeignKey(id => id.DescriptorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Drink)
            .WithMany()
            .HasForeignKey(r => r.ReviewedDrinkId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Source)
            .WithMany()
            .HasForeignKey(r => r.SourceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DescriptorReview>()
            .HasOne(dr => dr.Descriptor)
            .WithMany()
            .HasForeignKey(dr => dr.DescriptorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DescriptorReview>()
            .HasOne(dr => dr.Review)
            .WithMany()
            .HasForeignKey(dr => dr.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Suggestion>()
            .HasOne(s => s.Employee)
            .WithMany()
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Suggestion>()
            .HasOne(s => s.Cocktail)
            .WithMany()
            .HasForeignKey(s => s.CocktailId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
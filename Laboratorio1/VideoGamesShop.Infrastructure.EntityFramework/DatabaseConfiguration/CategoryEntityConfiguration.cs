using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGamesShop.Core.Entities;

namespace VideoGamesShop.Infrastructure.EntityFramework.DatabaseConfiguration
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Code);
            modelBuilder.Property(x => x.Code).ValueGeneratedOnAdd();
            modelBuilder.Property(x => x.Name).IsRequired();
            modelBuilder.HasMany(x => x.VideoGames).WithOne(x => x.Category).HasForeignKey(x => x.CategoryCode);    
        }
    }
}

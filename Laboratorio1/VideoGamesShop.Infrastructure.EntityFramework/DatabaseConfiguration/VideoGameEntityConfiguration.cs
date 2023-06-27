using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGamesShop.Core.Entities;

namespace VideoGamesShop.Infrastructure.EntityFramework.DatabaseConfiguration
{
    public class VideoGameEntityConfiguration : IEntityTypeConfiguration<VideoGame>
    {
        public void Configure(EntityTypeBuilder<VideoGame> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Property(x => x.Name).IsRequired();
            modelBuilder.Property(x => x.PublicationDate).IsRequired();
            modelBuilder.Property(x => x.Author).IsRequired();
            modelBuilder.Property(x => x.GameMode).IsRequired();
            modelBuilder.Property(x => x.CopiesCount).IsRequired();

            modelBuilder.HasOne(x => x.Category).WithMany(x => x.VideoGames).HasForeignKey(x => x.CategoryCode);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Core.Entities;
using VideoGamesShop.Infrastructure.EntityFramework.DatabaseConfiguration;

namespace VideoGamesShop.Infrastructure.EntityFramework
{
    public class VideoGamesShopContext: DbContext
    {
        public VideoGamesShopContext(DbContextOptions<VideoGamesShopContext> options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<ShopRecord> ShopRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Category>(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration<VideoGame>(new VideoGameEntityConfiguration());
            modelBuilder.ApplyConfiguration<ShopRecord>(new ShopRecordEntityConfiguration());
        }
    }
}

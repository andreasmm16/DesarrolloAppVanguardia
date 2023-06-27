using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VideoGamesShop.Core.Entities;

namespace VideoGamesShop.Infrastructure.EntityFramework.DatabaseConfiguration
{
    public class ShopRecordEntityConfiguration : IEntityTypeConfiguration<ShopRecord>
    {
        public void Configure(EntityTypeBuilder<ShopRecord> modelBuilder)
        {
            modelBuilder.HasKey(x => x.RecordId);
            modelBuilder.Property(x => x.RecordId).ValueGeneratedOnAdd();
            modelBuilder.Property(x => x.Date).IsRequired();
            modelBuilder.Property(x => x.EmployeeName).IsRequired();
            modelBuilder.Property(x => x.Operation).IsRequired();  
        }
    }
}

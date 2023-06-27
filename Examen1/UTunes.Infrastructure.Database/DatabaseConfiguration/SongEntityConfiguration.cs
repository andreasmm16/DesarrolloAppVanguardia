using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTunes.Core.Entities;

namespace UTunes.Infrastructure.Database.DatabaseConfiguration
{
    public class SongEntityConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Artist).IsRequired();
            builder.HasOne(x => x.Album)
                .WithMany(x => x.Songs)
                .HasForeignKey(x => x.AlbumId);

            builder.HasData(new Song
            {
                Name = "Don't go breaking my heart",
                Artist = "Backstreet boys",
                AlbumId = 1,
                Id = 1,
                SongPrice=100.00
            },
            new Song
            {
                Name = "Nobody else",
                Artist = "Backstreet boys",
                AlbumId = 1,
                Id = 2,
                SongPrice = 150.00
            },
            new Song
            {
                Name = "Breathe",
                Artist = "Backstreet boys",
                AlbumId = 1,
                Id = 3,
                SongPrice = 98.00
            },
            new Song
            {
                Name = "New love",
                Artist = "Backstreet boys",
                AlbumId = 1,
                Id = 4,
                SongPrice = 98.00
            },
            new Song
            {
                Name = "Rock Me",
                Artist = "One Direction",
                AlbumId = 2,
                Id = 5,
                SongPrice=150.00
            },
            new Song
            {
                Name = "Danger",
                Artist = "BTS",
                AlbumId = 3,
                Id = 6,
                SongPrice = 180.00
            },
            new Song
            {
                Name = "Do you think it makes sense?",
                Artist = "BTS",
                AlbumId = 3,
                Id = 7,
                SongPrice = 253.00
            }
            );

        }
    }
}


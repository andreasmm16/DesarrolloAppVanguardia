using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using UTunes.Core.Entities;

namespace UTunes.Core.AlbumManager
{


    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> albumRepository;
        private readonly IRepository<Song> songRepository;

        public AlbumService(IRepository<Album> albumRepository, IRepository<Song> songRepository)
        {
            this.albumRepository = albumRepository;
            this.songRepository = songRepository;
        }

        public OperationResult<Album> DislikeAlbum(int id)
        {
            var album = this.albumRepository.GetById(id);
            if (album is null)
            {
                return new OperationResult<Album>(new Error
                {
                    Code = ErrorCode.BadRequest,
                    Message = $"No se encontró un album con el id: {id}"
                });
            }
            album.Dislikes++;
            albumRepository.Update(album);
            albumRepository.Commit();
            return album;
        }

        public async Task<OperationResult<IReadOnlyList<Album>>> GetAllAsync() =>
            (await this.albumRepository.AllAsync
            ()).ToList();

        public OperationResult<AlbumExtended> GetExtendedAlbumInfo(int id)
        {
            var album = this.albumRepository.GetById(id);
            if (album is null)
            {
                return new OperationResult<AlbumExtended>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = $"Album con id {id} no existe"
                });
            }
            var albumPrice = this.songRepository.Filter(x => x.AlbumId == id).Sum(x => x.SongPrice);
            double albumScore = 0;
            if ((album.Likes + album.Dislikes) > 0)
            {
                albumScore = (double)album.Likes / (double)(album.Likes + album.Dislikes);
            }
            else
            {
                albumScore = 0;
            }

            var songList = this.songRepository.Filter(x => x.AlbumId == id)
         .Select(song => new SongAlbumInfo
         {
             Name = song.Name,
             Artist = song.Artist,
             SongPrice = song.SongPrice
         })
       .ToList();
            return new OperationResult<AlbumExtended>(new AlbumExtended
            {
                Name = album.Name,
                Artist = album.Artist,
                Review = album.Review,
                Songs = songList,
                Score = albumScore,
                AlbumPrice = albumPrice
            });

        }

        public OperationResult<SongAlbumInfo> GetSongById(int albumId, string name)
        {
            var album = this.albumRepository.GetById(albumId);
            if (album is null)
            {
                return new OperationResult<SongAlbumInfo>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = $"Album con id {albumId} no existe"
                });
            }
            var songsList = this.songRepository.Filter(x => x.AlbumId == albumId)
                .Select(song => new SongAlbumInfo
                {
                    Name = song.Name,
                    Artist = song.Artist,
                    SongPrice = song.SongPrice
                }
                )
                .ToList();

            var song = songsList.FirstOrDefault(x => x.Name == name);
            if (song == null)
            {
                return new OperationResult<SongAlbumInfo>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = $"El álbum con id {albumId} no tiene la canción {name}"
                });
            }
            return song;
        }

        public OperationResult<Album> LikeAlbum(int id)
        {
            var album = this.albumRepository.GetById(id);
            if (album is null)
            {
                return new OperationResult<Album>(new Error
                {
                    Code = ErrorCode.BadRequest,
                    Message = $"No se encontró un album con el id: {id}"
                });
            }
            album.Likes++;
            albumRepository.Update(album);
            albumRepository.Commit();
            return album;
        }
    }
}

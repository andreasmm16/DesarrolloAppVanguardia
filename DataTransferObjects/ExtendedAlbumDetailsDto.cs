using UTunes.Core.Entities;

namespace UTunes.Api.DataTransferObjects
{
    public class ExtendedAlbumDetailsDto
    {
        public string Name { get; set; }

        public string Artist { get; set; }

        public string Review { get; set; }

        public ICollection<SongAlbumInfo> Songs { get; set; }

        public double AlbumPrice { get; set; }
        public double Score { get; set; }
    }
}

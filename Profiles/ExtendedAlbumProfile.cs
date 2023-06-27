using AutoMapper;
using UTunes.Api.DataTransferObjects;

namespace UTunes.Api.Profiles
{
    public class ExtendedAlbumProfile:Profile
    {
        public ExtendedAlbumProfile()
        {
            CreateMap<Core.Entities.AlbumExtended, ExtendedAlbumDetailsDto>();
        }
    }
}

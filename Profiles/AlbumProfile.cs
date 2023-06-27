using AutoMapper;

namespace UTunes.Api.Profiles
{
    public class AlbumProfile:Profile
    {
        public AlbumProfile()
        {
            CreateMap<Core.Entities.Album, AlbumDetailsDto>();
        }
        
    }
}

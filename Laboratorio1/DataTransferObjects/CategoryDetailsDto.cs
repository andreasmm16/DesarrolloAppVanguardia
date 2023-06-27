using VideoGamesShop.Core.Entities;

namespace VideoGamesShop.Api.DataTransferObjects
{
    public class CategoryDetailsDto
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public ICollection<VideoGame> VideoGames { get; set; }
    }
}

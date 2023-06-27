using VideoGamesShop.Core;

namespace VideoGamesShop.Api.DataTransferObjects
{
    public class CreateVideoGameDto
    {
        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Author { get; set; }
        public string GameMode { get; set; }
        public int CopiesCount { get; set; }
    }
}

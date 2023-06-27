using VideoGamesShop.Core;

namespace VideoGamesShop.Api.DataTransferObjects
{
    public class VideoGameDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Author { get; set; }
        public string GameMode { get; set; } //Cambiar por Enum
        public int CopiesCount { get; set; }
        public int CategoryCode { get; set; }
    }
}

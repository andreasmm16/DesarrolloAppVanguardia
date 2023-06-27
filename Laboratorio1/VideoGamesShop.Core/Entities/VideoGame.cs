namespace VideoGamesShop.Core.Entities
{
    public class VideoGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Author { get; set; }
        public string GameMode { get; set; } 
        public int CopiesCount { get; set; }
        public int CategoryCode { get; set; }
        public Category? Category { get; set; }
    }
}

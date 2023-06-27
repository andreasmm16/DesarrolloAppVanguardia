using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGamesShop.Core.Entities
{
    public class Category
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public ICollection<VideoGame>? VideoGames { get; set; } = new HashSet<VideoGame>();
    }
}

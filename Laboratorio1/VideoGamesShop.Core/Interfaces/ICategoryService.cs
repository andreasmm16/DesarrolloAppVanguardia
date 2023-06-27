using SocialNetwork.Core;
using VideoGamesShop.Core.Entities;

namespace VideoGamesShop.Core.Interfaces
{
    public interface ICategoryService
    {
        OperationResult<Category> Create(Category category);
        OperationResult<IEnumerable<Category>> GetAll();
        OperationResult<Category> GetById(int categoryCode);

        OperationResult<IEnumerable<VideoGame>> GetVideoGames(int categoryCode);
    }
}

using SocialNetwork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Core.Entities;

namespace VideoGamesShop.Core.Interfaces
{
    public interface IVideoGameService
    {
        OperationResult<VideoGame> Create(int categoryId, VideoGame category);
        OperationResult<IEnumerable<VideoGame>> GetAll();
        OperationResult<VideoGame> GetById(int gameId);
        OperationResult<VideoGame> RentVideoGame(int gameId);
        OperationResult<VideoGame> ReturnVideoGame(int gameId);
        OperationResult<IEnumerable<VideoGame>> VideoGamesByGameMode(string gameMode);
    }
}

using SocialNetwork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Core.Entities;
using VideoGamesShop.Core.Interfaces;

namespace VideoGamesShop.Infrastructure
{
    public class VideoGameService : IVideoGameService
    {
        private readonly IRepository<VideoGame> videoGameRepository;
        private readonly IRepository<Category> categoryRepository;

        public VideoGameService(IRepository<VideoGame> videoGameRepository, IRepository<Category> categoryRepository)
        {
            this.videoGameRepository = videoGameRepository;
            this.categoryRepository = categoryRepository;
        }

        public OperationResult<VideoGame> Create(int categoryId,VideoGame videogame)
        {
            var category = this.categoryRepository.GetById(categoryId);
            if(category is null)
            {
                return new OperationResult<VideoGame>(new Error
                {
                    Code = ErrorCode.BadRequest,
                    Message = $"No se encontró una categoría con el código {category.Code}"
                });
            }
            videogame.Category = category;
            this.videoGameRepository.Add(videogame);
            return new OperationResult<VideoGame>(videogame);
        }

        public OperationResult<IEnumerable<VideoGame>> GetAll()
        {
            var gameslist = videoGameRepository.Get().ToList();
            return new OperationResult<IEnumerable<VideoGame>>(gameslist);
        }

        public OperationResult<VideoGame> GetById(int gameId)
        {
            var videogame = this.videoGameRepository.GetById(gameId);
            if (videogame is null)
            {
                return new OperationResult<VideoGame>(new Error
                {
                    Code = ErrorCode.BadRequest,
                    Message = $"No se encontró un videojuego con el código {gameId}"
                });
            }

            return videogame;
        }

        public OperationResult<VideoGame> RentVideoGame(int gameId)
        {
            var videogame = videoGameRepository.GetById(gameId);
            if(videogame is null)
            {
                return new OperationResult<VideoGame>(new Error
                {
                    Code = ErrorCode.BadRequest,
                    Message = $"No se encontró un videojuego con el código {gameId}"
                });
            }
            if(videogame.CopiesCount < 3)
            {
                return new OperationResult<VideoGame>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = $"No hay suficientes copias disponibles"
                });
            }
            videogame.CopiesCount--;
            videoGameRepository.Update(videogame);
            
            return videogame;
        }

        public OperationResult<VideoGame> ReturnVideoGame(int gameId)
        {
            var videogame = videoGameRepository.GetById(gameId);
            if (videogame is null)
            {
                return new OperationResult<VideoGame>(new Error
                {
                    Code = ErrorCode.BadRequest,
                    Message = $"No se encontró un videojuego con el código {gameId}"
                });
            }
            videogame.CopiesCount++;
            videoGameRepository.Update(videogame);

            return videogame;
        }

        public OperationResult<IEnumerable<VideoGame>> VideoGamesByGameMode(string gameMode)
        {
            if(gameMode != "Multi Player" && gameMode != "Multi player" && gameMode != "Single Player" && gameMode != "Single player")
            {
                return new OperationResult<IEnumerable<VideoGame>>(new Error
                {
                    Code = ErrorCode.BadRequest,
                    Message = $"No existe el modo de juego {gameMode}"
                });
            }

            var gamesList = videoGameRepository.Filter(x => x.GameMode == gameMode).ToList();
            return new OperationResult<IEnumerable<VideoGame>>(gamesList);
           
        }
    }
}

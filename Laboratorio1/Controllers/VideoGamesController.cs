using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core;
using VideoGamesShop.Api.DataTransferObjects;
using VideoGamesShop.Core;
using VideoGamesShop.Core.Entities;
using VideoGamesShop.Core.Interfaces;

namespace VideoGamesShop.Api.Controllers
{
    [ApiController]
    public class VideoGamesController : ControllerBase
    {
        private readonly IVideoGameService videoGameService;
        private readonly IShopRecordService shopRecordService;
        private readonly ErrorResult errorResult;

        public VideoGamesController(IVideoGameService videoGameService, IShopRecordService shopRecordService, ErrorResult errorResult)
        {
            this.videoGameService = videoGameService;
            this.shopRecordService = shopRecordService;
            this.errorResult = errorResult;
        }

        /// <summary>
        /// Agrega un videojuego nuevo
        /// </summary>
        /// <param name="categoryCode">Id de la categoría a la que pertenece el juego</param>
        /// <param name="videogameDto">Videojuego a agregarse</param>
        /// <returns>Información del videojuego creado</returns>
        [HttpPost("categories/{categoryCode}/[controller]")]
        public ActionResult CreateVideoGame([FromRoute] int categoryCode, [FromBody] CreateVideoGameDto videogameDto)
        {
            var videoGame = new VideoGame
            {
                Name = videogameDto.Name,
                PublicationDate = videogameDto.PublicationDate,
                Author = videogameDto.Author,
                GameMode = videogameDto.GameMode,
                CopiesCount = videogameDto.CopiesCount,
                CategoryCode = categoryCode
            };
            var result = this.videoGameService.Create(categoryCode,videoGame);
            if (result.Succeeded)
            {
                return Ok(new VideoGameDetailsDto
                {
                    Id = result.Result.Id,
                    Name = videogameDto.Name,
                    PublicationDate = videoGame.PublicationDate,
                    Author = videogameDto.Author,
                    GameMode= videogameDto.GameMode,
                    CopiesCount = videogameDto.CopiesCount,
                    CategoryCode = categoryCode
                });
            }
            return errorResult.GetErrorResult(result);
        }

        /// <summary>
        /// Obtiene todos los videojuegos
        /// </summary>
        /// <returns>Listado de los videojuegos</returns>
        [HttpGet("categories/[controller]")]
        public ActionResult GetVideoGames()
        {
            
            return Ok(this.videoGameService.GetAll().Result.Select(x => new VideoGameDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                PublicationDate = x.PublicationDate,
                Author = x.Author,
                GameMode = x.GameMode,
                CopiesCount = x.CopiesCount,
                CategoryCode = x.CategoryCode
            }));
        }

        /// <summary>
        /// Obtiene un videojuego según su id
        /// </summary>
        /// <param name="gameId">Id del videojuego</param>
        /// <returns>Información del videojuego según el Id ingresado</returns>
        [HttpGet("categories/[controller]/{gameId}")]
        public ActionResult GetVideoGameById([FromRoute] int gameId)
        {
            var videogame = this.videoGameService.GetById(gameId);
            if (videogame.Succeeded)
            {
                return Ok(new VideoGameDetailsDto
                {
                    Id = videogame.Result.Id,
                    Name = videogame.Result.Name,
                    PublicationDate = videogame.Result.PublicationDate,
                    Author = videogame.Result.Author,
                    GameMode = videogame.Result.GameMode,
                    CopiesCount = videogame.Result.CopiesCount,
                    CategoryCode = videogame.Result.CategoryCode
                });
            }
            return errorResult.GetErrorResult(videogame);
        }

        /// <summary>
        /// Prestar/Rentar un videojuego
        /// </summary>
        /// <param name="gameId">Id del videojuego a prestar</param>
        /// <param name="employeeName">Nombre del empleado que lo rentó</param>
        /// <returns></returns>
        [HttpPut("categories/rent_game/{gameId}")]
        public ActionResult RentVideoGame([FromRoute]int gameId, [FromBody] string employeeName)
        {
            var rentGame = this.videoGameService.RentVideoGame(gameId);
            if (rentGame.Succeeded)
            {
                this.shopRecordService.PostRecord(new ShopRecord
                {
                    Date = DateTime.Now,
                    EmployeeName = employeeName,
                    VideoGameId = rentGame.Result.Id,
                    Operation = "Rent"
                });
                return Ok(new VideoGameDetailsDto
                {
                    Id = rentGame.Result.Id,
                    Name = rentGame.Result.Name,
                    PublicationDate = rentGame.Result.PublicationDate,
                    Author = rentGame.Result.Author,
                    GameMode = rentGame.Result.GameMode,
                    CopiesCount = rentGame.Result.CopiesCount,
                    CategoryCode = rentGame.Result.CategoryCode
                });
            }
            return errorResult.GetErrorResult(rentGame);
        }

        /// <summary>
        /// Devolver un videojuego
        /// </summary>
        /// <param name="gameId">Id del videojuego a retornar</param>
        /// <param name="employeeName">Nombre del empleado de la operación de devolución del videojuego</param>
        /// <returns></returns>
        [HttpPut("categories/return_game/{gameId}")]
        public ActionResult ReturnVideoGame([FromRoute] int gameId, [FromBody] string employeeName)
        {
            var returnGame = this.videoGameService.ReturnVideoGame(gameId);
            if (returnGame.Succeeded)
            {
                this.shopRecordService.PostRecord(new ShopRecord
                {
                    Date = DateTime.Now,
                    EmployeeName = employeeName,
                    VideoGameId = returnGame.Result.Id,
                    Operation = "Return"
                });
                return Ok(new VideoGameDetailsDto
                {
                    Id = returnGame.Result.Id,
                    Name = returnGame.Result.Name,
                    PublicationDate = returnGame.Result.PublicationDate,
                    Author = returnGame.Result.Author,
                    GameMode = returnGame.Result.GameMode,
                    CopiesCount = returnGame.Result.CopiesCount,
                    CategoryCode = returnGame.Result.CategoryCode
                });
            }
            return errorResult.GetErrorResult(returnGame);
        }

        /// <summary>
        /// Obtener los videojuegos según su modo de Juego --> Multi Player o Single Player
        /// </summary>
        /// <param name="gamemode">Modo de juego: Multi Player o Single Player</param>
        /// <returns></returns>
        [HttpGet("gamemodes/{gamemode}")]
        public ActionResult GetVideoGamesByGameMode([FromRoute] string gamemode)
        {
            var games = this.videoGameService.VideoGamesByGameMode(gamemode);
            if (games.Succeeded)
            {
                return Ok(games.Result.Select(x => new VideoGameDetailsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PublicationDate = x.PublicationDate,
                    Author = x.Author,
                    GameMode = x.GameMode,
                    CopiesCount = x.CopiesCount,
                    CategoryCode = x.CategoryCode
                }));
            }
            return BadRequest($"Modo de Juego {gamemode} no existe");
        }
    }
}

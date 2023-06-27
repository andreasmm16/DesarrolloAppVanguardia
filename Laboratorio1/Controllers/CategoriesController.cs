using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core;
using VideoGamesShop.Api.DataTransferObjects;
using VideoGamesShop.Core.Entities;
using VideoGamesShop.Core.Interfaces;

namespace VideoGamesShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase

    {
        private readonly ICategoryService categoryService;
        private readonly ErrorResult errorResult;

        public CategoriesController(ICategoryService categoryService, ErrorResult errorResult)
        {
            this.categoryService = categoryService;
            this.errorResult = errorResult;
        }

        /// <summary>
        /// Crear una categoría
        /// </summary>
        /// <param name="categoryDto">La categoría que se agregará</param>
        /// <returns>La categoría agregada</returns>
        [HttpPost]
        public ActionResult CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };
            var result = this.categoryService.Create(category);
            if (result.Succeeded)
            {
                return Ok(new CategoryDetailsDto
                {
                    Name= categoryDto.Name,
                    Code = result.Result.Code,
                    VideoGames = result.Result.VideoGames
                });
            }
            return errorResult.GetErrorResult(result);
        }

        /// <summary>
        /// Obtener información de categoría por id o todas las categorías
        /// </summary>
        /// <param name="categoryCode">Código de la categoría</param>
        /// <returns>Información de la categoría</returns>
        [HttpGet]
        public ActionResult GetCategoryById([FromQuery] int? categoryCode)
        {
            if (!categoryCode.HasValue)
            {
                return Ok(this.categoryService.GetAll().Result.Select(x => new CategoryListDto
                {
                    Code = x.Code,
                    Name = x.Name
                }));
            }
            var category = this.categoryService.GetById((int)categoryCode);
            if (category.Succeeded)
            {
                return Ok(category.Result);
            }
            return errorResult.GetErrorResult(category);
        }
        /// <summary>
        /// Obtener los videojuegos por categoría
        /// </summary>
        /// <param name="categoryId">El id de la categoria </param>
        /// <returns>La lista de los videosjuegos de la categoría ingresadas</returns>
        [HttpGet("{categoryId}/videogames")]
        public ActionResult GetVideoGamesByCategory([FromRoute] int categoryId)
        {
            var games = this.categoryService.GetVideoGames(categoryId);
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
            return BadRequest($"Categoría con id {categoryId} no existe");
        }
    }
}

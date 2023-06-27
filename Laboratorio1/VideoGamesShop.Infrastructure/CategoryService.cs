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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<VideoGame> videoGameRepository;

        public CategoryService(IRepository<Category> categoryRepository, IRepository<VideoGame> videoGameRepository)
        {
            this.categoryRepository = categoryRepository;
            this.videoGameRepository = videoGameRepository;
        }
        public OperationResult<Category> Create(Category category)
        {
            this.categoryRepository.Add(category);
            return new OperationResult<Category>(category);
        }

        public OperationResult<IEnumerable<Category>> GetAll()
        {
            var categoriesList = categoryRepository.Get().ToList();
            return new OperationResult<IEnumerable<Category>>(categoriesList);
        }

        public OperationResult<Category> GetById(int categoryCode)
        {
            var category = this.categoryRepository.GetById(categoryCode);
            if (category is null)
            {
                return new OperationResult<Category>(new Error
                {
                    Code = ErrorCode.BadRequest,
                    Message = $"No se encontró una categoría con el código {category.Code}"
                });
            }

            return category;

        }

        public OperationResult<IEnumerable<VideoGame>> GetVideoGames(int categoryCode)
        {
            var category = this.categoryRepository.GetById(categoryCode);
            if (category is null)
            {
                return new OperationResult<IEnumerable<VideoGame>>(new Error
                {
                    Code = ErrorCode.BadRequest,
                    Message = $"No se encontró una categoría con el código {categoryCode}"
                });
            }
            var gamesList = videoGameRepository.Filter(x => x.CategoryCode == categoryCode).ToList();
            return new OperationResult<IEnumerable<VideoGame>>(gamesList);
        }
            
    }
}

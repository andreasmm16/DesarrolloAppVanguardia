using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Api.DataTransferObjects;
using VideoGamesShop.Core.Interfaces;

namespace VideoGamesShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopRecordsController : ControllerBase
    {
        private readonly IShopRecordService shopRecordService;

        public ShopRecordsController(IShopRecordService shopRecordService)
        {
            this.shopRecordService = shopRecordService;
        }
        /// <summary>
        /// Lista todos los registros de los videojuegos prestados o devueltos
        /// </summary>
        /// <returns>Listado de los registros</returns>
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            return Ok(this.shopRecordService.GetAll().Result.Select(x => new ShopRecordDetailsDto
            {
                RecordId = x.RecordId,
                Date = x.Date,
                EmployeeName = x.EmployeeName,
                VideoGameId = x.VideoGameId,
                Operation = x.Operation
            }));
        }
    }
}

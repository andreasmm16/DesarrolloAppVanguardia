using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core;

namespace VideoGamesShop.Api
{
    [ApiController]
    public class ErrorResult : ControllerBase
    {
        public ActionResult GetErrorResult<TResult>(OperationResult<TResult> result)
        {
            switch (result.Error.Code)
            {
                case ErrorCode.NotFound:
                    return NotFound(result.Error.Message);
                case ErrorCode.Unauthorized:
                    return Unauthorized(result.Error.Message);
                default:
                    return BadRequest(result.Error.Message);
            }
        }
    }
}

using SocialNetwork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Core.Entities;

namespace VideoGamesShop.Core.Interfaces
{
    public interface IShopRecordService
    {
        OperationResult<ShopRecord> PostRecord(ShopRecord shopRecord);
        OperationResult<IEnumerable<ShopRecord>> GetAll();
    }
}

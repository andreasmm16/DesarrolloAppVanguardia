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
    public class ShopRecordService : IShopRecordService
    {
        private readonly IRepository<ShopRecord> shopRecordsRepository;

        public ShopRecordService(IRepository<ShopRecord> shopRecordsRepository)
        {
            this.shopRecordsRepository = shopRecordsRepository;
        }


        public OperationResult<IEnumerable<ShopRecord>> GetAll()
        {
            var recordsList = shopRecordsRepository.Get().ToList();
            return new OperationResult<IEnumerable<ShopRecord>>(recordsList);
        }


        public OperationResult<ShopRecord> PostRecord(ShopRecord shopRecord)
        {
            var shopRecordToAdd = this.shopRecordsRepository.Add(shopRecord);
            return new OperationResult<ShopRecord>(shopRecordToAdd);
        }
    }
}

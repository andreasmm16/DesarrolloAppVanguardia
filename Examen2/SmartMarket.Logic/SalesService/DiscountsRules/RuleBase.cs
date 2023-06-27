using SmartMarket.Logic.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Logic.SalesService.DiscountsRules
{
    //Design Pattern: Template Method
    public abstract class RuleBase
    {
        public abstract bool IsMatch(DateOnly date, string product);
        public virtual decimal NormalTotal(string product, int quantity, IEnumerable<StockItem> _stock)
        {
            var stockItem = _stock.First(x => x.ProductName == product);
            var total = stockItem.Price * quantity;
            if (stockItem.MembershipDeal is not null)
            {
                var numberOfDeals = quantity / stockItem.MembershipDeal.Quantity;
                var remainder = quantity % stockItem.MembershipDeal.Quantity;
                total = numberOfDeals * stockItem.MembershipDeal.Price + remainder * stockItem.Price;
            }
            return total;
        }
        public virtual decimal AddTotal(string product, int quantity, IEnumerable<StockItem> _stock)
        {
            return NormalTotal(product, quantity, _stock);
        }
    }
}

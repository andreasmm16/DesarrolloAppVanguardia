using SmartMarket.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Logic.SalesService.DiscountsRules
{
    public class SaturdaySProductDiscount : RuleBase
    {
        //Hacer match si el DateOnly es Sabado y el producto empieza con S
        public override bool IsMatch(DateOnly date,string product)=> date.DayOfWeek == DayOfWeek.Saturday && product.StartsWith("S");

        //hacer override del metodo AddTotal para aplicar el descuento
        //El descuento es de 10%
        public override decimal AddTotal(string product, int quantity, IEnumerable<StockItem> _stock)
        {
            var total = NormalTotal(product, quantity, _stock);
            total -= total * 0.1m;
            return total;
        }
    }
}

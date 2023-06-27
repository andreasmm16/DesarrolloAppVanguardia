using SmartMarket.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Logic.SalesService.DiscountsRules
{
    public class MonTuesDiscount : RuleBase
    {
        //Hacer match si el DateOnly es Lunes o Martes
        public override bool IsMatch(DateOnly date, string product) => date.DayOfWeek is DayOfWeek.Monday or DayOfWeek.Tuesday;
        
        //hacer override del metodo AddTotal para aplicar el descuento
        public override decimal AddTotal(string product, int quantity, IEnumerable<StockItem> _stock)
        {
            var total = NormalTotal(product, quantity, _stock);
            total -= total * 0.05m;
            return total;
        }        
    }
}

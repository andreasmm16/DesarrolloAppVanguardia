using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Logic.SalesService.DiscountsRules
{
    public class NormalDiscount : RuleBase
    {
        //Este metodo no hace match con ninguna fecha, por lo que siempre se aplica
        public override bool IsMatch(DateOnly date, string product) => true;
    }
}

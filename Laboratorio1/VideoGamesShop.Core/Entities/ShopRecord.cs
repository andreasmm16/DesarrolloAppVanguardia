using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGamesShop.Core.Entities
{
    public class ShopRecord
    {
        public int RecordId { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeName { get; set; }
        public int VideoGameId { get; set; }
        public string Operation { get; set; }//Si se rentó o prestó
    }
}

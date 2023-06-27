using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Logic.DatetimeManager
{
    public interface IDatetimeService
    {
        //Método que retorna la fecha actual
        DateOnly GetDate();
    }
}

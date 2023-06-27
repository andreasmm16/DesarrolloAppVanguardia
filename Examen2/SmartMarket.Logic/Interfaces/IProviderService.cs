using SmartMarket.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Logic.Interfaces
{
    public interface IProviderService
    {
        void Dispose();
        Task<Provider?> GetFromApiByIdAsync(Guid id);
    }
}

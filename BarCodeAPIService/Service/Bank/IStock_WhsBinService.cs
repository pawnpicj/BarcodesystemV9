using BarCodeLibrary.Respones.SAP.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service.Bank
{
    public interface IStock_WhsBinService
    {
        Task<ResponseGetStockByWhsBin> responseGetStockByWhsBin();
    }
}

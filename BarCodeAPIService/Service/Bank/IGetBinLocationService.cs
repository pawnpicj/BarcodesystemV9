using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service.Bank
{
    public interface IGetBinLocationService
    {
        Task<ResponseGetBinLocation> responseGetBinLocation(string whscode);
    }
}

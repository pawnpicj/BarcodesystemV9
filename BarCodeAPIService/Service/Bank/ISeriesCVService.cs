using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service.Bank
{
    public interface ISeriesCVService
    {
        Task<ResponseNNM1_CV> responseNNM1_CV();
    }
}

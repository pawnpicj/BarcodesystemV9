using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;

namespace BarCodeAPIService.Service.Pannreaksmey
{
    public interface IGenerateBinCodeServices
    {
        Task<ResponeNNG1GetGenerateBinCode> ResponeNNG1GetGenerateBinCode();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP.Pannreaksmey;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;

namespace BarCodeAPIService.Service.Pannreaksmey
{
    public interface IGenerateBinCodeServices
    {
        Task<ResponeNNG1GetGenerateBinCode> ResponeNNG1GetGenerateBinCode();
        Task<ResponseGetBinCodeGeneration> ResponseGetBinGeneration();
        Task<ResponseGetBinCodeGeneration> PostGenerationCode(SendBinLocationGenerate sendBinlocation);
    }
}

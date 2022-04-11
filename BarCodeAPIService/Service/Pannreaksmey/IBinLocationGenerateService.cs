using BarCodeLibrary.Request.SAP.Pannreaksmey;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BarCodeAPIService.Service.Pannreaksmey
{
    public interface IBinLocationGenerateService
    {
        Task<ResponseGetBinCodeGeneration> ResponseGenBinGeneration();
        Task<ResponseSaveGenerationCode> PostGenerationCode(SendBinLocationGenerate sendBinlocation);
    }
}

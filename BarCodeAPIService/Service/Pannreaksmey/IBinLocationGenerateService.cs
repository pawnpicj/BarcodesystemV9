using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP.Pannreaksmey;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;

namespace BarCodeAPIService.Service.Pannreaksmey
{
    public interface IBinLocationGenerateService
    {
        Task<ResponseGetBinCodeGeneration> ResponseGenBinGeneration();
        Task<ResponseSaveGenerationCode> PostGenerationCode(SendBinLocationGenerate sendBinlocation);
    }
}
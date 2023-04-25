using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IBinCodeService
    {
        Task<ResponseOBINGetBinCode> ResponseOBINGetBinCode();

        Task<ResponseOBINGetBinCode> ResponseGetBinCodeByCode(string binCode);
    }
}
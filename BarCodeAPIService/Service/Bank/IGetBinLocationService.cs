using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service.Bank
{
    public interface IGetBinLocationService
    {
        Task<ResponseGetBinLocation> responseGetBinLocation(string whscode);
    }
}
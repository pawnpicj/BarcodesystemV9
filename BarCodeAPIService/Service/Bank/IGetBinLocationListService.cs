using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service.Bank
{
    public interface IGetBinLocationListService
    {
        Task<ResponseGetBinLocationList> responseGetBinLocationList(string barcode, string itemcode);
    }
}
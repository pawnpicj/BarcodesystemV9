using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IPriceListService
    {
        Task<ResponseITM1GetPriceList> ResponseITM1GetPriceList();
    }
}
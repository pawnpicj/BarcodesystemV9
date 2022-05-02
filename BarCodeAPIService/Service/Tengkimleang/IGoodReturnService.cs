using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IGoodReturnService
    {
        Task<ResponseOPDNGetGoodReceipt> responseOPDNGetGoodReceipt();
        Task<ResponseGoodReturn> sendGoodReturn(SendGoodsReturn sendGoodReturn);
    }
}
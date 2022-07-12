using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IGoodReturnService
    {
        Task<ResponseOPDNGetGoodReceipt> responseOPDNGetGoodReceipt(string cardCode);
        Task<ResponseOPDNGetGoodReceipt> responseOPDNGetGoodReceiptByDocNum(string DocNum);
        Task<ResponseGoodReturn> sendGoodReturn(SendGoodsReturn sendGoodReturn);
    }
}
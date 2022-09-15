using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Vichika;

namespace BarCodeAPIService.Service
{
    public interface IDeliveryService
    {
        Task<ResponseGetORDR> responseGetORDR(string cardName);
        Task<ResponseGetBatch> responseGetBatch(string ItemCode,string WhsCode);
        Task<ResponseGetSerial> responseGetSerial(string ItemCode,string WhsCode);
        Task<ResponseDelivery> PostDelivery(SendDelivery sendDelivery);
    }
}
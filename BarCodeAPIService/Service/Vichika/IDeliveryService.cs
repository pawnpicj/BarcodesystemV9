using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IDeliveryService
    {
        Task<ResponseGetORDR> responseGetORDR();

        Task<ResponseGetORDRLine> responseGetORDRLine(int DocEntry);

        Task<ResponseDelivery> PostDelivery(SendDelivery sendDelivery);
    }
}
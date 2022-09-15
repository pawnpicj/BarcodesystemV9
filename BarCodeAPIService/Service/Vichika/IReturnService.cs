using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP.Vichika;
using BarCodeLibrary.Respones.SAP.Vichika;

namespace BarCodeAPIService.Service.Vichika
{
    public interface IReturnService
    {
        Task<ResponseODLNGetDelivery> responseODLNGetDelivery(string cardCode);
        Task<ResponseODLNGetDelivery> responseODLNGetDeliveryByDocNum(string DocNum);
        Task<ResponseReturn> sendGoodReturn(SendReturn sendGoodReturn);
    }
}

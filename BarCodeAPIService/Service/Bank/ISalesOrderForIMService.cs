using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Vichika;

namespace BarCodeAPIService.Service
{
    public interface ISalesOrderForIMService
    {
        Task<ResponseSalesOrder> PostSalesOrder(SendSalesOrderForIM sendSalesOrderForIM);
    }
}
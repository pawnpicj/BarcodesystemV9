using System.Collections.Generic;
using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP.Tengkimleang;
using BarCodeLibrary.Request.SAP.TengKimleang;
using BarCodeLibrary.Respones.SAP.Tengkimleang;

namespace BarCodeAPIService.Service
{
    public interface IGoodsReceiptPOService
    {
        Task<ResponseOPORGetPO> responseOPORGetPO(string cardName);
        Task<ResponseGoodReceiptPO> PostGoodReceiptPO(SendGoodReceiptPO sendGoodReceiptPO);
        Task<ResponseCustomerGet> responseCustomerGets();
        Task<ResponseGetSeries> responseGetSeries(string objectCode, string dateOfMonth);
        Task<ResponseGetSaleEmployee> responseGetSaleEmployees();
        Task<ResponseGetCurrency> responseGetCurrency(string cardCode);
        Task<ResponseGetGenerateBatchSerial> responseGetGenerateBatchSerial(GetGenerateSerialBatchRequest generateSerialBatchRequest);
        Task<ResponseGetItemCode> responseGetItemCodes();
        Task<ResponseGetVatCode> responseGetVatCodes();
        Task<ResponseGetWarehouse> responseGetWarehouses();
        Task<ResponseGetUnitOfMeasure> responseGetUnitOfMeasure();
    }
}
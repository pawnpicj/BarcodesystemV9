using BarCodeLibrary.Request.SAP.TengKimleang;
using BarCodeLibrary.Respones.SAP.Tengkimleang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public interface IGoodsReceiptPOService
    {
        Task<ResponseOPORGetPO> responseOPORGetPO(string cardName);
        Task<ResponseGoodReceiptPO> PostGoodReceiptPO(SendGoodReceiptPO sendGoodReceiptPO);
        Task<ResponseCustomerGet> responseCustomerGets();
        Task<ResponseGetSeries> responseGetSeries(string objectCode,string dateOfMonth);
        Task<ResponseGetSaleEmployee> responseGetSaleEmployees();

    }
}

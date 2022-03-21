using BarCodeLibrary.Respones.SAP.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service.Bank
{
    public interface IStock_WhsBinService
    {
        Task<ResponseGetStockByWhsBin> responseGetStockByWhsBin(string whsCode, string binCode);

        Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemBatch(string itemCode,  string batchNumber);
        Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemSerial(string itemCode, string serialNumber);

        Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemBatchBin(string itemCode, string batchNumber, string binEntry);
        Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemSerialBin(string itemCode, string serialNumber, string binEntry);
    }
}

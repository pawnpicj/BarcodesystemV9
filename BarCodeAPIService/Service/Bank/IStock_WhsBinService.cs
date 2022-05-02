using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP.Bank;

namespace BarCodeAPIService.Service.Bank
{
    public interface IStock_WhsBinService
    {
        Task<ResponseGetStockByWhsBin> responseGetStockByWhsBin(string whsCode, string binCode);

        Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemBatch(string itemCode, string batchNumber);
        Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemSerial(string itemCode, string serialNumber);

        Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemBatchBin(string itemCode, string batchNumber,
            string binEntry);

        Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemSerialBin(string itemCode, string serialNumber,
            string binEntry);

        Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemBatchW(string itemCode, string batchNumber,
            string whsCode);

        Task<ResponseGetStockItemBatchAndSerial> responseGetStockItemSerialW(string itemCode, string serialNumber,
            string whsCode);
    }
}
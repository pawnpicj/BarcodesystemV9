using BarCodeLibrary.Respones.SAP.Tengkimleang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service.Tengkimleang
{
    public interface IStockDataService
    {
        Task<ResponseGetStockBatchSerial> responseGetStockBatchSerial(string BatchCode,string Serial, string BarCode);
        Task<ResponseGetStockBatchSerialWarehouse> responseGetStockBatchSerialWarehouse(string BatchCode,string Serial, string BarCode,string Warehouse);
        Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialBinCode(string BatchCode,string Serial, string BarCode,string Warehouse,string BinCode);
        Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerial(string ItemCode);
        Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerial(string ItemCode,string WhsCode);
        Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialBinCode(string ItemCode,string WhsCode,string BinCode);
        Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialWarehouseCode(string WhsCode);
        Task<ResponseGetStcokBatchSerialBinCode> responseGetStockBatchSerialWarehouseCode(string WhsCode, string BinCode);
    }
}

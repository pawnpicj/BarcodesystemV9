using BarCodeAPIService.Service.Tengkimleang;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers.Tengkimleang
{
    public class StockDataController : ControllerBase
    {
        private readonly IStockDataService stockDataService;

        public StockDataController(IStockDataService stockDataService)
        {
            this.stockDataService = stockDataService;
        }
        [HttpGet("GetStcokBatchSerial/{BarCode}/{Serial}/{BatchCode}")]
        public async Task<IActionResult> GetStockBatchSerialAsync(string BarCode,string Serial,string BatchCode)
        {
            var a = await stockDataService.responseGetStockBatchSerial(BatchCode,Serial,BarCode);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpGet("GetStcokBatchSerialWarehouse/{BarCode}/{Serial}/{BatchCode}/{Warehouse}")]
        public async Task<IActionResult> GetStcokBatchSerialWarehouseAsync(string BarCode, string Serial, string BatchCode,string Warehouse)
        {
            var a = await stockDataService.responseGetStockBatchSerialWarehouse(BatchCode, Serial, BarCode,Warehouse);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpGet("GetStcokBatchSerialBinCode/{BarCode}/{Serial}/{BatchCode}/{Warehouse}")]
        public async Task<IActionResult> GetStcokBatchSerialBinCodeAsync(string BarCode, string Serial, string BatchCode, string Warehouse,string BinCode)
        {
            var a = await stockDataService.responseGetStockBatchSerialBinCode(BatchCode, Serial, BarCode, Warehouse,BinCode);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpGet("GetStockItemBatchSerial/{ItemCode}/{BatchCode}/{Serial}")]
        public async Task<IActionResult> GetStockItemBatchSerialAsync(string ItemCode, string BatchCode, string Serial)
        {
            var a = await stockDataService.responseGetStockItemBatchSerial(ItemCode, BatchCode, Serial);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
    }
}

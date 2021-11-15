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
        [HttpGet("GetStcokBatchSerial/{BarCode}/{Serail}/{BatchCode}")]
        public async Task<IActionResult> GetStockBatchSerialAsync(string BarCode,string Serail,string BatchCode)
        {
            var a = await stockDataService.responseGetStockBatchSerial(BatchCode,Serail,BarCode);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpGet("GetStcokBatchSerialWarehouse/{BarCode}/{Serail}/{BatchCode}/{Warehouse}")]
        public async Task<IActionResult> GetStcokBatchSerialWarehouseAsync(string BarCode, string Serail, string BatchCode,string Warehouse)
        {
            var a = await stockDataService.responseGetStockBatchSerialWarehouse(BatchCode, Serail, BarCode,Warehouse);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpGet("GetStcokBatchSerialBinCode/{BarCode}/{Serail}/{BatchCode}/{Warehouse}")]
        public async Task<IActionResult> GetStcokBatchSerialBinCodeAsync(string BarCode, string Serail, string BatchCode, string Warehouse,string BinCode)
        {
            var a = await stockDataService.responseGetStockBatchSerialBinCode(BatchCode, Serail, BarCode, Warehouse,BinCode);
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

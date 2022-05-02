using System.Threading.Tasks;
using BarCodeAPIService.Service.Bank;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers.Bank
{
    public class Stock_WhsBinController : ControllerBase
    {
        private readonly IStock_WhsBinService stockWhsBinService;

        public Stock_WhsBinController(IStock_WhsBinService stockWhsBinService)
        {
            this.stockWhsBinService = stockWhsBinService;
        }

        [HttpGet("GetStockWhsBin/{whsCode}/{binCode}")]
        public async Task<IActionResult> GetStockWhsBinAsync(string whsCode, string binCode)
        {
            var a = await stockWhsBinService.responseGetStockByWhsBin(whsCode, binCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetStockItemBatchBin/{itemCode}/{batchNumber}/{binEntry}")]
        public async Task<IActionResult> GetStockItemBatchBinAsync(string itemCode, string batchNumber, string binEntry)
        {
            var a = await stockWhsBinService.responseGetStockItemBatchBin(itemCode, batchNumber, binEntry);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetStockItemSerialBin/{itemCode}/{serialNumber}/{binEntry}")]
        public async Task<IActionResult> GetStockItemSerialBinAsync(string itemCode, string serialNumber,
            string binEntry)
        {
            var a = await stockWhsBinService.responseGetStockItemSerialBin(itemCode, serialNumber, binEntry);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetStockItemBatch/{itemCode}/{batchNumber}")]
        public async Task<IActionResult> GetStockItemBatchAsync(string itemCode, string batchNumber)
        {
            var a = await stockWhsBinService.responseGetStockItemBatch(itemCode, batchNumber);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetStockItemSerial/{itemCode}/{serialNumber}")]
        public async Task<IActionResult> GetStockItemSerialAsync(string itemCode, string serialNumber)
        {
            var a = await stockWhsBinService.responseGetStockItemSerial(itemCode, serialNumber);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetStockItemBatchW/{itemCode}/{batchNumber}/{whsCode}")]
        public async Task<IActionResult> GetStockItemBatchWAsync(string itemCode, string batchNumber, string whsCode)
        {
            var a = await stockWhsBinService.responseGetStockItemBatchW(itemCode, batchNumber, whsCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetStockItemSerialW/{itemCode}/{serialNumber}/{whsCode}")]
        public async Task<IActionResult> GetStockItemSerialWAsync(string itemCode, string serialNumber, string whsCode)
        {
            var a = await stockWhsBinService.responseGetStockItemSerialW(itemCode, serialNumber, whsCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}
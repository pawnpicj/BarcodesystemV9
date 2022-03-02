using BarCodeAPIService.Service;
using BarCodeAPIService.Service.Pannreaksmey;
using BarCodeAPIService.Service.Bank;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers.Bank
{
    public class Stock_WhsBinController : ControllerBase
    {
        private IStock_WhsBinService stockWhsBinService;

        public Stock_WhsBinController(IStock_WhsBinService stockWhsBinService)
        {
            this.stockWhsBinService = stockWhsBinService;
        }
        [HttpGet("GetStockWhsBin/{whsCode}/{binCode}")]
        public async Task<IActionResult> GetStockWhsBinAsync(string whsCode, string binCode)
        {
            var a = await stockWhsBinService.responseGetStockByWhsBin(whsCode, binCode);
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

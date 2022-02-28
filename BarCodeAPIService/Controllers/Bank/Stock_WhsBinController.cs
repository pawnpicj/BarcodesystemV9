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
    [ApiController]
    [Route(APIRoute.Root)]
    public class Stock_WhsBinController : ControllerBase
    {
        private IStock_WhsBinService stockWhsBin;

        public Stock_WhsBinController(IStock_WhsBinService stockWhsBin)
        {
            this.stockWhsBin = stockWhsBin;
        }

        [HttpGet]
        public async Task<IActionResult> GetStock_WhsBinAsync()
        {
            var a = await stockWhsBin.responseGetStockByWhsBin();
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

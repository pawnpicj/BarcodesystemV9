using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class PriceListController : Controller
    {
        private readonly IPriceListService priceList;

        public PriceListController(IPriceListService priceList)
        {
            this.priceList = priceList;
        }
        [HttpGet]
        public async Task<IActionResult> GetPriceList()
        {
            var a = await priceList.ResponseITM1GetPriceList();
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

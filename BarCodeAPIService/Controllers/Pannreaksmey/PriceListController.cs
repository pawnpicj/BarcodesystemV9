using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class PriceListController : ControllerBase
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
                return Ok(a);
            return BadRequest(a);
        }
    }
}
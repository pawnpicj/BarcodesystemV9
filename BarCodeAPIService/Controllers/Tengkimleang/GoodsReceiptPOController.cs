using System.Threading.Tasks;
using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP.TengKimleang;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route(APIRoute.Root)]
    public class GoodsReceiptPOController : ControllerBase
    {
        private readonly IGoodsReceiptPOService goodsReceiptPO;

        public GoodsReceiptPOController(IGoodsReceiptPOService goodsReceiptPO)
        {
            this.goodsReceiptPO = goodsReceiptPO;
        }

        [HttpGet(APIRoute.GoodReceiptPO.GetPO + "{cardName}")]
        public async Task<IActionResult> GetGoodsReceiptPOAsync(string cardName)
        {
            var a = await goodsReceiptPO.responseOPORGetPO(cardName);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpPost(APIRoute.GoodReceiptPO.SendGoodReceiptPO)]
        public async Task<IActionResult> PostGoodReceiptPOAsync(SendGoodReceiptPO sendGoodReceiptPO)
        {
            var a = await goodsReceiptPO.PostGoodReceiptPO(sendGoodReceiptPO);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet(APIRoute.GoodReceiptPO.GetCustomer)]
        public async Task<IActionResult> GetCustomer()
        {
            var a = await goodsReceiptPO.responseCustomerGets();
            return Ok(a);
        }

        [HttpGet(APIRoute.GoodReceiptPO.GetSeries + "{objectCode}/{dateOfMonth}")]
        public async Task<IActionResult> GetSeriesAsync(string objectCode, string dateOfMonth)
        {
            var a = await goodsReceiptPO.responseGetSeries(objectCode, dateOfMonth);
            return Ok(a);
        }

        [HttpGet(APIRoute.GoodReceiptPO.GetSaleEmployee)]
        public async Task<IActionResult> GetSaleEmployeeAsync()
        {
            var a = await goodsReceiptPO.responseGetSaleEmployees();
            return Ok(a);
        }

        [HttpGet(APIRoute.GoodReceiptPO.GetCurrency + "{cardCode}")]
        public async Task<IActionResult> GetCurrencyAsync(string cardCode)
        {
            var a = await goodsReceiptPO.responseGetCurrency(cardCode);
            return Ok(a);
        }
    }
}
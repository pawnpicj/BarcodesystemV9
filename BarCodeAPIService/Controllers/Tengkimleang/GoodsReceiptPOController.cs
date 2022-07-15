using System.Threading.Tasks;
using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP.Tengkimleang;
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

        //[HttpGet(APIRoute.GoodReceiptPO.GetGoodReturn + "{cardName}")]
        //public async Task<IActionResult> GetGoodReturnAsync(string cardName)
        //{
        //    var a = await goodsReceiptPO.responseORPDGetGoodReturn(cardName);
        //    if (a.ErrorCode == 0)
        //        return Ok(a);
        //    return BadRequest(a);
        //}

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

        [HttpPost(APIRoute.GoodReceiptPO.GetGenerate_Serial_Batch)]
        public async Task<IActionResult> GetGenerate_Serial_Batch(GetGenerateSerialBatchRequest generateSerialBatchRequest)
        {
            var a = await goodsReceiptPO.responseGetGenerateBatchSerial(generateSerialBatchRequest);
            return Ok(a);
        }

        [HttpGet(APIRoute.GoodReceiptPO.GetItemCode)]
        public async Task<IActionResult> GetItemCodeAsyncTask()
        {
            var a = await goodsReceiptPO.responseGetItemCodes();
            return Ok(a);
        }

        [HttpGet(APIRoute.GoodReceiptPO.GetTaxCode)]
        public async Task<IActionResult> GetTaxCode()
        {
            var a = await goodsReceiptPO.responseGetVatCodes();
            return Ok(a);
        }
        [HttpGet(APIRoute.GoodReceiptPO.GetWarehouse)]
        public async Task<IActionResult> GetWarehouse()
        {
            var a = await goodsReceiptPO.responseGetWarehouses();
            return Ok(a);
        }
        [HttpGet(APIRoute.GoodReceiptPO.GetUnitOfMeasure + "{itemCode}")]
        public async Task<IActionResult> GetUnitOfMeasure(string itemCode)
        {
            var a = await goodsReceiptPO.responseGetUnitOfMeasure(itemCode);
            return Ok(a);
        }
        [HttpPost(APIRoute.GoodReceiptPO.GetBatchGenerator)]
        public async Task<IActionResult> GetBatchGenTask(GetBatchGenRequest generateSerialBatchRequest)
        {
            var a = await goodsReceiptPO.responseGetGenerateBatchAsync(generateSerialBatchRequest);
            return Ok(a);
        }
        [HttpGet(APIRoute.GoodReceiptPO.GetBarCodeItem+"{BarCode}")]
        public async Task<IActionResult> GetBarCodeItem(string BarCode)
        {
            var a = await goodsReceiptPO.responseGetBarCode(BarCode);
            return Ok(a);
        }
    }
}
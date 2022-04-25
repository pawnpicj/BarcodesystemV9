using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Request.SAP.TengKimleang;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpPost(APIRoute.GoodReceiptPO.SendGoodReceiptPO)]
        public async Task<IActionResult> PostGoodReceiptPOAsync(SendGoodReceiptPO sendGoodReceiptPO)
        {
            var a = await goodsReceiptPO.PostGoodReceiptPO(sendGoodReceiptPO);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpGet(APIRoute.GoodReceiptPO.GetCustomer)]
        public async Task<IActionResult> GetCustomer()
        {
            var a = await goodsReceiptPO.responseCustomerGets();
            return Ok(a);
        }
    }
}

using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
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

        [HttpGet("GetPO")]
        public async Task<IActionResult> GetGoodsReceiptPOAsync()
        {
            var a =await  goodsReceiptPO.responseOPDNGetPO();
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpPost("SendGoodReceiptPO")]
        public async Task<IActionResult> PostGoodReceiptPOAsync(SendGoodReceiptPO sendGoodReceiptPO)
        {
            var a=await goodsReceiptPO.responseGoodReceiptPO(sendGoodReceiptPO);
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

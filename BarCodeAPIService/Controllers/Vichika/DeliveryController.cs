using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace BarCodeAPIService.Controllers
{
    
    [ApiController]
    [Route(APIRoute.Root)]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService Delivery;
        public DeliveryController(IDeliveryService Delivery)
        {
            this.Delivery = Delivery;
        }
        [HttpGet("GetSO")]
        public async Task<IActionResult> GetDeliveryAsyc()
        {
            var a = await Delivery.responseGetORDR();
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpPost("SendDelivery")]
        public async Task<IActionResult> PostDeliveryAsync(SendDelivery sendDelivery)
        {
            var a = await Delivery.PostDelivery(sendDelivery);
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

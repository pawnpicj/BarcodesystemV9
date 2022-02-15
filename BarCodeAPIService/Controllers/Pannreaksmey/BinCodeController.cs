using Barcodesystem.Contract.RouteApi;
using BarCodeAPIService.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class BinCodeController : ControllerBase
    {
        private readonly IBinCodeService binCode;

        public BinCodeController(IBinCodeService binCode)
        {
            this.binCode = binCode;
        }
        [HttpGet("GetBinCode")]
        public async Task<IActionResult> GetBinCodeAnsync()
        {
            var a = await binCode.ResponseOBINGetBinCode();
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

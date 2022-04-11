
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BarCodeAPIService.Service.Pannreaksmey;
using BarCodeLibrary.Request.SAP.Pannreaksmey;
using Barcodesystem.Contract.RouteApi;

namespace BarCodeAPIService.Controllers.Pannreaksmey
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class GenerateBinCodeController : Controller
    {
        private readonly IGenerateBinCodeServices generateBinCode;
       public GenerateBinCodeController(IGenerateBinCodeServices generateBinCode)
       {
            this.generateBinCode = generateBinCode;
       }
        [HttpGet("GetGenerateBinCode")]
        public async Task<IActionResult> GetGenerateBinCodeAsync()
        {
            var a = await generateBinCode.ResponeNNG1GetGenerateBinCode();
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpPost("SendGenerateBinLocation")]
        public async Task<IActionResult> PostGenerationCodeAsync(SendBinLocationGenerate sendBinCode)
        {
            var a = await generateBinCode.PostGenerationCode(sendBinCode);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpGet("GetBinGenerated")]
        public async Task<IActionResult> getBinGenerated()
        {
            var a = await generateBinCode.ResponseGetBinGeneration();
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

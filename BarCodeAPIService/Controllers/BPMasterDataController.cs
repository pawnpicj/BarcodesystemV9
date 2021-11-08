using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class BPMasterDataController : Controller
    {     
        private readonly IBPMasterDataService bPMasterData;
        public BPMasterDataController(IBPMasterDataService bPMasterData) {
            this.bPMasterData = bPMasterData;    
        }
        [HttpGet("GetBP")]
        public async Task<IActionResult> GetBPMasterDataAsync() {
            var a = await bPMasterData.ResponseOCRDGetBP();
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else {
                return BadRequest(a);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;

namespace BarCodeAPIService.Controllers
{
    public class BPMasterDataController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [ApiController]
        [Route(APIRoute.Root)]
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

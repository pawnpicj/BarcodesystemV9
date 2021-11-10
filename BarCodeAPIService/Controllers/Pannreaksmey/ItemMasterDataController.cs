using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class ItemMasterDataController : ControllerBase
    {
        private readonly IItemMasterDataService itemMasterData;

        public ItemMasterDataController(IItemMasterDataService itemMasterData) {
            this.itemMasterData = itemMasterData;
        }
        [HttpGet("GetItemMasterData")]
        public async Task<IActionResult> GetItemMasterDataAnsync()
        {
            var a = await itemMasterData.ResponseOITMGetItemMaster();
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

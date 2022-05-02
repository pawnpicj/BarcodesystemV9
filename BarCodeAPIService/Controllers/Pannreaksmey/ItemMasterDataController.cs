using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class ItemMasterDataController : ControllerBase
    {
        private readonly IItemMasterDataService itemMasterData;

        public ItemMasterDataController(IItemMasterDataService itemMasterData)
        {
            this.itemMasterData = itemMasterData;
        }

        [HttpGet("GetItemMasterData")]
        public async Task<IActionResult> GetItemMasterDataAnsync()
        {
            var a = await itemMasterData.ResponseOITMGetItemMaster();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}
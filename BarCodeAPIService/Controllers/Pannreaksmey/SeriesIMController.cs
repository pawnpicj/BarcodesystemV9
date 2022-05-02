using System.Threading.Tasks;
using BarCodeAPIService.Service.Pannreaksmey;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers.Pannreaksmey
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class SeriesIMController : ControllerBase
    {
        private readonly ISeriesIMService seriesIM;

        public SeriesIMController(ISeriesIMService seriesIM)
        {
            this.seriesIM = seriesIM;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeriesIMAsync()
        {
            var a = await seriesIM.responseNNM1_IM();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}
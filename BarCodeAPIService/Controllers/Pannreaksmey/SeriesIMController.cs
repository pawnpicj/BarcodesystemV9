using BarCodeAPIService.Service;
using BarCodeAPIService.Service.Pannreaksmey;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers.Pannreaksmey
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class SeriesIMController : ControllerBase
    {
        private ISeriesIMService seriesIM;

        public SeriesIMController(ISeriesIMService seriesIM)
        {
            this.seriesIM = seriesIM;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeriesIMAsync()
        {
            var a = await seriesIM.responseNNM1_IM();
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

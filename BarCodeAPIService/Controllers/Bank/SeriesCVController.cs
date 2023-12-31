﻿using System.Threading.Tasks;
using BarCodeAPIService.Service.Bank;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers.Bank
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class SeriesCVController : ControllerBase
    {
        private readonly ISeriesCVService seriesCV;

        public SeriesCVController(ISeriesCVService seriesCV)
        {
            this.seriesCV = seriesCV;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeriesCVAsync()
        {
            var a = await seriesCV.responseNNM1_CV();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetSeriesCode/{yymm}/{typeSeries}")]
        public async Task<IActionResult> GetSeriesCodeAsync(string yymm, string typeSeries)
        {
            var a = await seriesCV.responseGetSeriesCode(yymm, typeSeries);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}
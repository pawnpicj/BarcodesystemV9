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
    public class DocumentSeriesController : ControllerBase
    {
        private readonly IDocumentSeriesService documentSeries;

        public DocumentSeriesController(IDocumentSeriesService documentSeries)
        {
            this.documentSeries = documentSeries;
        }
        [HttpGet]
        public async Task<IActionResult> GetDocumentSeries()
        {
            var a = await documentSeries.responseNNM1GetDocumentSeries();
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

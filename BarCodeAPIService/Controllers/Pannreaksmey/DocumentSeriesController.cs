using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

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
                return Ok(a);
            return BadRequest(a);
        }
    }
}
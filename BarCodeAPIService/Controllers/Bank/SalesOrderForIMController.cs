using System.Threading.Tasks;
using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class SalesOrderForIMController : ControllerBase
    {
        private readonly ISalesOrderForIMService salesOrderForIM;

        public SalesOrderForIMController(ISalesOrderForIMService salesOrderForIM)
        {
            this.salesOrderForIM = salesOrderForIM;
        }

        [HttpPost("SendSalesOrderForIM")]
        public async Task<IActionResult> PostSalesOrderForIMAsync(SendSalesOrderForIM sendSalesOrderForIM)
        {
            var a = await salesOrderForIM.PostSalesOrder(sendSalesOrderForIM);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

    }
}
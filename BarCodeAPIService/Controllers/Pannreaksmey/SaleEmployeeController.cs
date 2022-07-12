using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class SaleEmployeeController : ControllerBase
    {
        private readonly ISaleEmployeeService saleEmployee;

        public SaleEmployeeController(ISaleEmployeeService saleEmployee)
        {
            this.saleEmployee = saleEmployee;
        }

        [HttpGet]
        public async Task<IActionResult> GetSaleEmployee()
        {
            var a = await saleEmployee.ResponseOSLPGetSalesEmployee();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}
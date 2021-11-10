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
    public class SaleEmployeeController : Controller
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

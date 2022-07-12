using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barcodesystem.Contract.RouteApi;
using BarCodeAPIService.Service.Pannreaksmey;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;

namespace BarCodeAPIService.Controllers.Pannreaksmey
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService department;
        public DepartmentController(IDepartmentService department)
        {
            this.department = department;
        }
        [HttpGet("GetDepartment")]
        public async Task<IActionResult> GetDepartmentAsync()
        {
            var a = await department.ResponseDepartment();
            return Ok(a);
        }
        [HttpPost("PostDepartment")]
        public async Task<IActionResult> PostDepartmentAsync(DEP dep)
        {
            var a = await department.ResponsePostDepartment(dep);
            return Ok(a);
        }
    }
}

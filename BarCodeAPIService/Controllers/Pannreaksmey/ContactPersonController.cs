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
    public class ContactPersonController : ControllerBase
    {
        private readonly IContactPersonService contactPerson;

        public ContactPersonController(IContactPersonService contactPerson)
        {
            this.contactPerson = contactPerson;
        }
        [HttpGet]
        public async Task<IActionResult> GetContactPerson() {
            var a = await contactPerson.ResponseOCPRGetContactPerson();
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

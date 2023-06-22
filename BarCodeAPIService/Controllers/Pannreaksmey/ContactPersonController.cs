using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetContactPerson()
        {
            var a = await contactPerson.ResponseOCPRGetContactPerson();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetCustomerC")]
        public async Task<IActionResult> GetCustomerCAnsync()
        {
            var a = await contactPerson.ResponseOCRDGetCustomer();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}
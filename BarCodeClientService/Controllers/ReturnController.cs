using BarCodeLibrary.APICall;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Request.SAP.Vichika;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Vichika;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeClientService.Controllers
{
    public class ReturnController : Controller
    {
        public IActionResult CreateReturn(string DocNum)
        {
            ViewBag.DocNum = DocNum;
            return View();
        }

        #region Http
        [HttpGet]
        public IActionResult GetReturnByCardCodeAsResult()
        {
            var a = API.Read<ResponseODLNGetDelivery>(APIRoute.Return.Controller + APIRoute.Return.GetDelivery + "");
            if (a.ErrorCode == 0)
            {
                return Ok(a.Data);
            }
            else
            {
                return BadRequest(a.ErrorMessage);
            }
        }

        [HttpGet]
        public IActionResult GetGoodReciptPoByDocNumResult(string DocNum)
        {
            var a = API.Read<ResponseODLNGetDelivery>(APIRoute.Return.Controller + APIRoute.Return.GetDeliveryByDocNum + DocNum);
            if (a.ErrorCode == 0)
            {
                return Ok(a.Data);
            }
            else
            {
                return BadRequest(a.ErrorMessage);
            }
        }

        [HttpPost]
        public IActionResult CreateGoodReturn(SendReturn sendReturn)
        {
            var a = API.PostWithReturn<ResponseReturn>(APIRoute.Return.Controller + APIRoute.Return.SendReturn, sendReturn);
            if (a.ErrorCode == 0)
            {
                return Ok(a.DocEntry);
            }
            else
            {
                return BadRequest(a.ErrorMsg);
            }
        }

        #endregion
    }
}

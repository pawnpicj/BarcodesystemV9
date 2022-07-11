using BarCodeLibrary.APICall;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeClientService.Controllers
{
    public class GoodReturnController : Controller
    {
        public IActionResult CreateGoodReturn(string DocNum)
        {
            ViewBag.DocNum = DocNum;
            return View();
        }

        #region Http
        [HttpGet]
        public IActionResult GetGoodReturnByCardCode(string cardCode)
        {
            var a = API.Read<ResponseOPDNGetGoodReceipt>(APIRoute.GoodReturn.Controller + APIRoute.GoodReturn.GetGoodReceiptPOByDocNum + cardCode);
            return Ok(a);
            //if (a.ErrorCode == 0)
            //{
            //    return Ok(a.Data);
            //}
            //else
            //{
            //    return BadRequest(a.ErrorMessage);
            //}
        }

        [HttpGet]
        public IActionResult GetGoodReciptPoByDocNumResult(string DocNum)
        {
            var a = API.Read<ResponseOPDNGetGoodReceipt>(APIRoute.GoodReturn.Controller + APIRoute.GoodReturn.GetGoodReceiptPOByDocNum+ DocNum);
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
        public IActionResult CreateGoodReturn(SendGoodsReturn sendGoodsReturn)
        {
            var a = API.PostWithReturn<ResponseGoodReturn>(APIRoute.GoodReturn.Controller + APIRoute.GoodReturn.SendGoodsReturn,sendGoodsReturn);
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

using BarCodeLibrary.APICall;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Tengkimleang;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP.Tengkimleang;
using BarCodeLibrary.Request.SAP.TengKimleang;

namespace BarCodeClientService.Controllers
{
    public class GoodsReceiptPOController : Controller
    {
        public IActionResult CreatePO()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetCustomerClientResult()
        {
            var a = API.Read<ResponseCustomerGet>(APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.GetCustomer);
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
        public IActionResult GetPurchaseOrderByCardCode(string cardCode)
        {
            var a = API.Read<ResponseOPORGetPO>(APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.GetPO + cardCode);
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
        public IActionResult GetSeries(string objectCode,string dateOfMonth)
        {
            var a = API.Read<ResponseGetSeries>(APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.GetSeries + objectCode + "/" + dateOfMonth);
            if(a.ErrorCode== 0)
            {
                return Ok(a.Data);
            }
            else
            {
                return BadRequest(a.ErrorMessage);
            } 
        }
        [HttpGet]
        public IActionResult GetSaleEmployeeResult()
        {
            var a = API.Read<ResponseGetSaleEmployee>(APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.GetSaleEmployee);
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
        public IActionResult GetCurrency(string cardCode)
        {
            var a= API.Read<ResponseGetCurrency>(APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.GetCurrency+cardCode);
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
        public IActionResult GetSerialBatchResult(GetGenerateSerialBatchRequest generateSerialBatchRequest)
        {
            var a = API.PostWithReturn<ResponseGetGenerateBatchSerial>(APIRoute.GoodReceiptPO.Controller+APIRoute.GoodReceiptPO.GetGenerate_Serial_Batch
                                                                        ,generateSerialBatchRequest);
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
        public IActionResult CreateGoodsReceiptPoResult(SendGoodReceiptPO goodReceiptPo)
        {
            var a = API.PostWithReturn<ResponseGetGenerateBatchSerial>(APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.SendGoodReceiptPO
                , goodReceiptPo);
            if (a != null)
            {
                return Ok(a.Data);
            }
            return BadRequest(API.ErrorMessage);
        }

        [HttpGet]
        public IActionResult GetItemActionResult()
        {
            var a = API.Read<ResponseGetItemCode>(
                APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.GetItemCode);
            if (a != null)
            {
                return Ok(a);
            }
            return BadRequest(API.ErrorMessage);
        }

        [HttpGet]
        public IActionResult GetTaxCodeResult()
        {
            var a = API.Read<ResponseGetVatCode>(APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.GetTaxCode);
            if (a != null)
            {
                return Ok(a.Data);
            }
            return BadRequest(API.ErrorMessage);
        }
        [HttpGet]
        public IActionResult GetWarehouseResult()
        {
            var a = API.Read<ResponseGetVatCode>(APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.GetWarehouse);
            if (a != null)
            {
                return Ok(a.Data);
            }
            return BadRequest(API.ErrorMessage);
        }

        [HttpGet]
        public IActionResult GetUomCodeResult(string ItemCode)
        {
            var a = API.Read<ResponseGetVatCode>(APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.GetUnitOfMeasure+ItemCode);
            if (a != null)
            {
                return Ok(a.Data);
            }
            return BadRequest(API.ErrorMessage);
        }

        [HttpGet]
        public IActionResult GenerateBatchResult(GenerateBatchRequest generateSerialBatchRequest)
        {
            var a = API.PostWithReturn<ResponseGetGenerateBatchSerial>(APIRoute.GoodReceiptPO.Controller + APIRoute.GoodReceiptPO.GetGenerate_Serial_Batch
                , generateSerialBatchRequest);
            if (a.ErrorCode == 0)
            {
                return Ok(a.Data);
            }
            else
            {
                return BadRequest(a.ErrorMessage);
            }
        }
    }
}

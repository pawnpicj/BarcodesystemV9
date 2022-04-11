
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Web;
using Rotativa.AspNetCore;
using BarCodeLibrary.Respones.SAP;
using BarCodeClientService;

namespace BarCodeClientService.Controllers
{
    public class BinLocationController : Controller
    {
                                                                                                                                                
        public IActionResult GBinLocation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetWarehouseWhs()
        {
            var a = API.Read<ResponseOWHSGetWarehouse>("api/Warehouse/GetWarehouse");
            return Ok(a);
        }
        [HttpGet]
        public IActionResult GetBinLocation()
        {
            var a = API.Read<ResponseOBINGetBinCode>("api/BinCode/GetBinCode");
            return Ok(a);
        }
        [HttpGet]
        public IActionResult ItemMaster()
        {
            return View();
        }
        public IActionResult GetItemMaster()
        {
            var a = API.Read<ResponseOITMGetItemMaster>("api/ItemMasterData/GetItemMasterData");
            return Ok(a);
        }
        [HttpGet]
        public IActionResult GetGenerateBinCode()
        {
            var a = API.Read < ResponeNNG1GetGenerateBinCode > ("api/GenerateBinCode/GetGenerateBinCode");
            return Ok(a);
        }
 
        public IActionResult PrintItemLablePDF()
        {
            ResponsePrintItemLable responsePrintItemLable = new ResponsePrintItemLable();
            responsePrintItemLable.Data = PrintItemLableStatic.Data;
            PrintItemLableStatic.Data = null;
            return new ViewAsPdf(responsePrintItemLable)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 5, Bottom = 0, Right = 5, Top = 20 },
                PageWidth = 264,
                PageHeight = 170,
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };
        }
       [HttpPost]
        public IActionResult PrintItemLablePDFAction(ResponsePrintItemLable print)
        {
            PrintItemLableStatic.Data = print.Data;
            //var json = new Rotativa.AspNetCore.ViewAsPdf("PrintItemLablePDF", printLabel);
            //return json;
            return Ok(1);
            // return View();
        }
    }
}

using BarCodeLibrary.APICall;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace BarCodeClientService.Controllers
{
    public class CreateLabelStickerController : Controller
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
            var a = API.Read<ResponeNNG1GetGenerateBinCode>("api/GenerateBinCode/GetGenerateBinCode");
            return Ok(a);
        }

        // Method for Pint Item Label's size 6x7
        public IActionResult PrintItemLablePDF()
        {
            var responsePrintItemLable = new ResponsePrintItemLable();
            responsePrintItemLable.Data = PrintItemLableStatic.Data;
            PrintItemLableStatic.Data = null;
            //4.5cm=170.0787401575px
            //7cm=264.5669291339px
            //6cm=226.7716535433px
            return new ViewAsPdf(responsePrintItemLable)
            {
                PageSize = Size.A4,
                PageMargins = { Left = 3, Bottom = 0, Right = 3, Top = 5 },
                PageWidth = 65,
                PageHeight = 180000
                //CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };
        }

        // Method for Pint Item Label's size 4.5x7
        public IActionResult PrintItemLablePDFSecon()
        {
            var responsePrintItemLable = new ResponsePrintItemLable();
            responsePrintItemLable.Data = PrintItemLableStatic.Data;
            //PrintItemLableStatic.Data = null;
            //4.5cm=170.0787401575px
            //7cm=264.5669291339px
            //6cm=226.7716535433px
            return new ViewAsPdf(responsePrintItemLable)
            {
                PageSize = Size.A4,
                PageMargins = { Left = 0, Bottom = 0, Right = 0, Top = 5 },
                PageWidth = 65,
                PageHeight = 180000
                // CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };
            //return View(responsePrintItemLable);
        }

        [HttpPost]
        public IActionResult PrintItemLablePDFAction(ResponsePrintItemLable print)
        {
            PrintItemLableStatic.Data = print.Data;
            return Ok(1);
        }

        //Print Bin Label Sticker
        //4cm=151.1811023622px
        //10cm=377.9527559055px
        public IActionResult PrintBinLabelPDF()
        {
            var responsePrintBinLabel = new ResponsePrintBinLabel();
            responsePrintBinLabel.Data = ResponsePrintLabelStatic.Data;
            return new ViewAsPdf(responsePrintBinLabel)
            {
                PageSize = Size.A4,
                PageMargins = { Left = 0, Bottom = 0, Right = 0, Top = 20 },
                PageWidth = 65,
                PageHeight = 1000000,
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };
            // return View(responsePrintBinLabel);
        }

        [HttpPost]
        public IActionResult PrintBinLabelPDFAction(ResponsePrintBinLabel print)
        {
            ResponsePrintLabelStatic.Data = print.Data;
            return Ok(1);
        }
    }
}
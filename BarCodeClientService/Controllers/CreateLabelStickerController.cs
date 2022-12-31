
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
using BarCodeLibrary.Respones.SAP.Bank;

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

        [HttpGet]
        public IActionResult GetItemByBarcode(string barCode, string itemCode)
        {
            var a = API.Read<ResponseGetStockItemBatchAndSerial>("GetItemByBarcode/" + barCode + "/" + itemCode);
            return Ok(a);
        }

        [HttpGet]
        public IActionResult GetUOMList()
        {
            var a = API.Read<ResponseGetOUOM>("GetUOMList");
            return Ok(a);
        }

        [HttpGet]
        public IActionResult GetUOMList2()
        {
            var a = API.Read<ResponseGetOUOM>("GetUOMList2");
            return Ok(a);
        }

        // Method for Pint Item Label's size 6x7
        public IActionResult PrintItemLablePDF()
        {
            ResponsePrintItemLable responsePrintItemLable = new ResponsePrintItemLable();
            responsePrintItemLable.Data = PrintItemLableStatic.Data;
            PrintItemLableStatic.Data = null;
            //4.5cm=170.0787401575px
            //7cm=264.5669291339px
            //6cm=226.7716535433px
            return new ViewAsPdf(responsePrintItemLable)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.Letter,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(0, 0, 1, 0),
                PageWidth = 120,
                PageHeight = 90
                //CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };
        }
        //PrintItem3LablePDF
        public IActionResult PrintItem3LablePDF(int sizeSticker)
        {
            ResponsePrintItemLable responsePrintItemLable = new ResponsePrintItemLable();
            responsePrintItemLable.Data = PrintItemLableStatic.Data;
            PrintItemLableStatic.Data = null;
            int page_Height;
            if (sizeSticker % 3 != 0)
            {
                page_Height = ((sizeSticker / 3) + 1) * 90;
            }
            else
            {
                page_Height = (sizeSticker / 3) * 90;
            }

            
            //4.5cm=170.0787401575px
            //7cm=264.5669291339px
            //6cm=226.7716535433px
            return new ViewAsPdf(responsePrintItemLable)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.Letter,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(0, 0, 1, 0),
                PageWidth = 240,
                PageHeight = 90
                //CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };
        }
        // Method for Pint Item Label's size 4.5x7
        public IActionResult PrintItemLablePDFSecon()
        {
            ResponsePrintItemLable responsePrintItemLable = new ResponsePrintItemLable();
            responsePrintItemLable.Data = PrintItemLableStatic.Data;
            //PrintItemLableStatic.Data = null;
            //4.5cm=170.0787401575px
            //7cm=264.5669291339px
            //6cm=226.7716535433px
            return new ViewAsPdf(responsePrintItemLable)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageMargins = { Left = 0, Bottom = 0, Right = 0, Top = 0 },
                PageWidth = 40,
                PageHeight = 65
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
            ResponsePrintBinLabel responsePrintBinLabel = new ResponsePrintBinLabel();
            responsePrintBinLabel.Data = ResponsePrintLabelStatic.Data;
            return new ViewAsPdf(responsePrintBinLabel)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.Letter,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(0, 0, 1, 0),
                PageWidth = 120,
                PageHeight = 90
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

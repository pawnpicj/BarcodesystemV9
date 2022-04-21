using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Respones.SAP;
using QRCoder;
using System.Drawing;
using System.IO;

namespace BarCodeClientService.Controllers
{
    public class QRBarcodeController : Controller
    {
        public IActionResult CreateBinLocation()
        {

            return View();
        }
        public IActionResult GenerateQRCode(string itemcode,string itemname, string fda,string str)
        {
            string text, text1;
            if (itemcode == null)
            {
                text1 = "";
            }
            else
            {
                text1 = "- " + itemcode + "\n- " + itemname + "\n- " + fda;
            }
            text = text1 + str;
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q) ;
            QRCode qRCode = new QRCode(qRCodeData);
            Bitmap bitmap = qRCode.GetGraphic(15);
            var bitmapBytes = ConvertBitmapToBytes(bitmap);
            return File(bitmapBytes, "image/jpeg");
            text1 = "";
            str = "";
            text = "";
        }

        private byte[] ConvertBitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream ms=new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}

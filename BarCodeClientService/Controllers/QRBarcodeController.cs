using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using IronBarCode;
using Microsoft.AspNetCore.Hosting;
using System;

namespace BarCodeClientService.Controllers
{
    public class QRBarcodeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public QRBarcodeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult CreateBinLocation()
        {
            return View();
        }

        public IActionResult GenerateQRCode(string itemcode, string itemname, string fda, string str)
        {
            string text, text1;
            if (itemcode == null)
                text1 = "";
            else
                text1 = "- " + itemcode + "\n- " + itemname + "\n- " + fda;
            text = text1 + str;
            var qRCodeGenerator = new QRCodeGenerator();
            var qRCodeData = qRCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            var qRCode = new QRCode(qRCodeData);
            var bitmap = qRCode.GetGraphic(15);
            var bitmapBytes = ConvertBitmapToBytes(bitmap);
            return File(bitmapBytes, "image/jpeg");
            text1 = "";
            str = "";
            text = "";
        }

        private byte[] ConvertBitmapToBytes(Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
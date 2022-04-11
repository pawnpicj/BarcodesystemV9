using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Pannreaksmey
{
   public class ResponsePrintItemLable
   {
      public List<OITMLable> Data { get; set; }
   }
    public static class PrintItemLableStatic
    {
        public static List<OITMLable> Data { get; set; }
    }
    public class OITMLable
    {
        public int No { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string ProductCode { get; set; }
        public string BatchSN { get; set; }
        public string ExpDate { get; set; }
        public string fda { get; set; }
    }
}

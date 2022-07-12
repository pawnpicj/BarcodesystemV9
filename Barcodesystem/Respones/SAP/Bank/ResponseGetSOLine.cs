using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class ResponseGetSOLine
    {
        public List<lSOL> Data { get; set; }
    }
    public static class SOLStatic
    {
        public static List<lSOL> Data { get; set; }
    }
    public class lSOL
    {
        public int docEntry { get; set; }
        public string docNum { get; set; }
        public string cardCode { get; set; }
        public string cardName { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public double lineTotal { get; set; }
        public string vatGroup { get; set; }
        public string whsCode { get; set; }
        public string discPrcnt { get; set; }
    }
}

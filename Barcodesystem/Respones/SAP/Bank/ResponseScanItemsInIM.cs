using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class ResponseScanItemsInIM
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetItemsLine> Data { get; set; }
    }
    public class GetItemsLine
    {
        public int DocEntry { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public string UOMCode { get; set; }

        public string FWhsCode { get; set; }
        public int FBinEntry { get; set; }
        public string FBinCode { get; set; }

        public string TWhsCode { get; set; }
        public int TBinEntry { get; set; }
        public string TBinCode { get; set; }

        public string BatchNumber { get; set; }
        public string SerialNumber { get; set; }
    }
}

using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class ResponseGetStockByWhsBin
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public List<LineStock> Data { get; set; }
    }

    public class LineStock
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public string BinCode { get; set; }
        public int BinEntry { get; set; }
        public string BatchNo { get; set; }
        public double Quantity { get; set; }
        public string UOMEntry { get; set; }
        public string UOMCode { get; set; }
        public string SerialNo { get; set; }
    }
}
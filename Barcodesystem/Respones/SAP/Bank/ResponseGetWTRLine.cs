using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseGetWTRLine
    {
        public int ErrorCode { get; set; }
        public string? ErrorMsg { get; set; }
        public List<WTRLine> Data { get; set; }
    }

    public class WTRLine
    {
        public int DocEntry { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public double Quantity { get; set; }
        public string FromWhsCod { get; set; }
        public string WhsCode { get; set; }
        public string UomCode { get; set; }
        public string unitMsr { get; set; }
        public double U_unitprice { get; set; }
        public string BinCode { get; set; }
        public int FromBinEntry { get; set; }
    }
}
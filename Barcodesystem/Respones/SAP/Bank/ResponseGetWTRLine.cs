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
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public double Quantity { get; set; }
        public double U_unitprice { get; set; }
        public string UomCode { get; set; }
        public string unitMsr { get; set; }
        public string Patient { get; set; }
        public string FromWhsCode { get; set; }
        public string FromBinCode { get; set; }
        public int FromBinEntry { get; set; }

        public string ToWhsCode { get; set; }
        public string ToBinCode { get; set; }
        public int ToBinEntry { get; set; }

        public string BatchYN { get; set; }
        public string SerialYN { get; set; }
    }
}
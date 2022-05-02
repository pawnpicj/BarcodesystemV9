using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseGetORDRLine
    {
        public int ErrorCode { get; set; }
        public string? ErrorMsg { get; set; }
        public List<ORDRLine> Data { get; set; }
    }

    public class ORDRLine
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double Quatity { get; set; }
        public double Price { get; set; }
        public double DiscPrcnt { get; set; }
        public string VatGroup { get; set; }
        public double LineTotal { get; set; }
        public string WhsCode { get; set; }
    }
}
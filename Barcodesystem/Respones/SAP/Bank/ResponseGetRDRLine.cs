using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class ResponseGetRDRLine
    {
        public int ErrorCode { get; set; }
        public string? ErrorMsg { get; set; }
        public List<RDRLine> Data { get; set; }
    }
    public class RDRLine
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


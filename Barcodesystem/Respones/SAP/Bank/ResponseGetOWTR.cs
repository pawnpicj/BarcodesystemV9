using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseGetOWTR
    {
        public int ErrorCode { get; set; }
        public string? ErrorMsg { get; set; }
        public List<OWTR> Data { get; set; }
    }
    public class OWTR
    {
        public int DocNum { get; set; }
        public int DocEntry { get; set; }
        public string DocDate { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string SlpCode { get; set; }
        public string SlpName { get; set; }
        public string FromWhs { get; set; }
        public string ToWhs { get; set; }
        public string SeriesName { get; set; }
        public int ToBinEntry { get; set; }
        public string ToBinCode { get; set; }
        public string LoanNum { get; set; }
        public List<WTR1> Line { get; set; }
    }
    public class WTR1
    {
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public double Quantity { get; set; }
        public string UomCode { get; set; }
        public string unitMsr { get; set; }
        public double U_unitprice { get; set; }
    }
}

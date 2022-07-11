using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseORPDGetGoodReturn
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<ORPD> Data { get; set; }
    }

    public class ORPD
    {
        public int DocEntry { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public int CntctCode { get; set; }
        public string NumAtCard { get; set; }
        public int DocNum { get; set; }
        public string DocStatus { get; set; }
        public string DocDate { get; set; }
        public string DocDueDate { get; set; }
        public string TaxDate { get; set; }
        public double DocTotal { get; set; }
        public double DiscPrcnt { get; set; }
        public double DiscountAMT { get; set; }
        public List<RPD1> Line { get; set; }
    }

    public class RPD1
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public double Quatity { get; set; }
        public double Price { get; set; }
        public double PriceBeforeDis { get; set; }
        public double DiscPrcnt { get; set; }
        public double DiscountAMT { get; set; }
        public string VatGroup { get; set; }
        public double LineTotal { get; set; }
        public string WhsCode { get; set; }
        public string ManageItem { get; set; }
        public string UomName { get; set; }
        public string TaxCode { get; set; }
        public int LineNum { get; set; }
    }
}

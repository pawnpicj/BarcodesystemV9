using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseOPORGetPO
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<OPOR> Data { get; set; }
    }
    public class OPOR
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
        public List<POR1> Line { get; set; }
    }
    public class POR1
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public double Quatity { get; set; }
        public double Price { get; set; }
        public double DiscPrcnt { get; set; }
        public string VatGroup { get; set; }
        public double LineTotal { get; set; }
        public string WhsCode { get; set; }
        public string ManageItem { get; set; }
    }
}

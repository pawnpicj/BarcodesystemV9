using System;
using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOPDNGetGoodReceipt
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<OPDN> Data { get; set; }
    }

    public class OPDN
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public int CntctCode { get; set; }
        public string NumAtCard { get; set; }
        public int DocNum { get; set; }
        public string DocStatus { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public DateTime TaxDate { get; set; }
        public double DocTotal { get; set; }
        public double DiscPrcnt { get; set; }
        public List<PDN1> Line { get; set; }
    }

    public class PDN1
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public double Quatity { get; set; }
        public double Price { get; set; }
        public double DiscPrcnt { get; set; }
        public string VatGroup { get; set; }
        public double LineTotal { get; set; }
        public string WhsCode { get; set; }
    }
}
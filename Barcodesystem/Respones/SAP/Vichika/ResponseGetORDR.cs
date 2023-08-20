using System;
using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseGetORDR
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<ORDR> Data { get; set; }
    }

    public class ORDR
    {
        public int DocEntry { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string CntctCode { get; set; }
        public string NumAtCard { get; set; }
        public string DocNum { get; set; }
        public string DocStatus { get; set; }
        public string DocDate { get; set; }
        public string DocDueDate { get; set; }
        public string TaxDate { get; set; }
        public double DocTotal { get; set; }
        public double DiscPrcnt { get; set; }
        public double DiscountAMT { get; set; }
        public string ToBinLocation { get; set; }
        public int SlpCode { get; set; }
        public string SlpName { get; set; }
        public List<RDR1> Line { get; set; }
    }

    public class RDR1
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public double Quatity { get; set; }
        public double InputQuantity { get; set; }        
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
        public double PriceAfVAT { get; set; }
        public string Patient { get; set; }
        public string TranferNo { get; set; }
        public int LineNum { get; set; }
    }
}
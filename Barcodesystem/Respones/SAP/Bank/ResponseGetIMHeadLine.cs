using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseGetIMHeadLine
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<OWTRIM> Data { get; set; }
    }

    public class OWTRIM
    {
        public string DocNum { get; set; }
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
        public string Patient { get; set; }
        public List<WTR1IM> Line { get; set; }
    }

    public class WTR1IM
    {
        public int DocEntry { get; set; }
        public int LineNum { get; set; }        
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double PriceBefDi { get; set; }
        public string WhsCode { get; set; }
        public int BinEntry { get; set; }
        public string BinCode { get; set; }
        public double LineTotal { get; set; }
        public int UomEntry { get; set; }
        public string UomCode { get; set; }
        public string FisrtBin { get; set; }
        public string IsBtchSerNum { get; set; }
        public string BatchSerialNumber { get; set; }
        public double BatchSerialQty { get; set; }
        public string Patient { get; set; }
    }
}
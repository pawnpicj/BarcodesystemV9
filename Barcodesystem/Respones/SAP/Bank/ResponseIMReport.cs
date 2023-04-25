using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseIMReport
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<RPT_OWTRIM> Data { get; set; }
    }

    public class RPT_OWTRIM
    {
        public string DocNum { get; set; } //int
        public int DocEntry { get; set; }
        public string DocDate { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string SlpCode { get; set; } //int
        public string SlpName { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public double Quantity { get; set; }
        public string UomCode { get; set; }
        public double Balance { get; set; }
        public double Price { get; set; }
        public double PriceBefDi { get; set; }
        public string WhsCode { get; set; }
        public double LineTotal { get; set; }
        public double DocTotal { get; set; }
        public string ExpDate { get; set; }
        public int BinEntry { get; set; } //int
        public string FisrtBin { get; set; }
        public string IsBtchSerNum { get; set; }
        public string BatchSerialNumber { get; set; }
        public double BatchSerialQTY { get; set; }        
    }
}
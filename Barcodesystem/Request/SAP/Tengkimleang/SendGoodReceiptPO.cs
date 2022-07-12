using System;
using System.Collections.Generic;

namespace BarCodeLibrary.Request.SAP.TengKimleang
{
    public class SendGoodReceiptPO
    {
        public string CardCode { get; set; }
        public string Series { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime TaxDate { get; set; }
        public string OrderNumber { get; set; }
        public string CurrencyCode { get; set; }
        public string SlpCode { get; set; }
        public string Remark { get; set; }
        public List<SendGoodReceiptPOLine> Line { get; set; }
    }

    public class SendGoodReceiptPOLine
    {
        public string ItemCode { get; set; }
        public double UnitPrice { get; set; }
        public double PriceBeforeDis { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public string UomName { get; set; }
        public string TaxCode { get; set; }
        public string Whs { get; set; }
        public string DocEntry { get; set; }
        public int LineNum { get; set; }
        public string ManageItem { get; set; }
        public List<Serial> Serial { get; set; }
        public List<Batch> Batches { get; set; }
    }

    public class Serial
    {
        public string SerialNumber { get; set; }
        public string MfrSerialNo { get; set; }
        public DateTime ExpDate { get; set; }
        public string Script { get; set; }
    }

    public class Batch
    {
        public DateTime AdmissionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ItemCode { get; set; }
        public DateTime MfrDate { get; set; }
        public int Qty { get; set; }
        public string SerialAndBatch { get; set; }
    }
}
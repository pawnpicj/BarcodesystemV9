using System;
using System.Collections.Generic;

namespace BarCodeLibrary.Request.SAP
{
    public class SendGoodsReturn
    {
        public int BaseEntry { get; set; }
        public string CardCode { get; set; }
        public string NumAtCard { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public string Remark { get; set; }
        public int Series { get; set; }
        public string BPCurrency { get; set; }

        public List<SendGoodsReturnLine> Lines { get; set; }
    }

    public class SendGoodsReturnLine
    {
        public string ItemCode { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public string TaxCode { get; set; }
        public string WarehouseCode { get; set; }
        public int UomCode { get; set; }
        public int LineNum { get; set; }
        public int BaseEntry { get; set; }
        public string ManageItem { get; set; }
    }
}
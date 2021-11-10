using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP
{
    public class SendGoodsReturn
    {
        public string CardCode { get; set; }
        public string NumAtCard { get; set; }
        public DateTime DocDate { get; set; }
        public int ContactPersion { get; set; }
        public string DocType { get; set; }
        public List<SendGoodsReturnLine> Lines { get; set; }
    }
    public class SendGoodsReturnLine{
        public string ItemCode { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public string TaxCode { get; set; }
        public string WarehouseCode { get; set; }
        public int UomCode { get; set; }
    }
}

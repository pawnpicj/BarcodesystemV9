using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP
{
    public class SendGoodReceiptPO
    {
        public string CardCode { get; set; }
        public DateTime DocDate { get; set; }
        public int BrandID { get; set; }
        public List<SendGoodReceiptPOLine> Line { get; set; }
    }
    public class SendGoodReceiptPOLine
    {
        public string ItemCode { get; set; }
        public int Qty { get; set; }
        public double UnitPrice { get; set; }
        public string WhsCode { get; set; }
        public int UomCode { get; set; }
    }

}

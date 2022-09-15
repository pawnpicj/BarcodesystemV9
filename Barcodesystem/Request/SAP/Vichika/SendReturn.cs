using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP.Vichika
{
    public class SendReturn
    {
        public int BaseEntry { get; set; }
        public string CardCode { get; set; }
        public string NumAtCard { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public string Remark { get; set; }
        public int Series { get; set; }
        public string BPCurrency { get; set; }
        public List<SendReturnLine> Lines { get; set; }
    }
    public class SendReturnLine
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

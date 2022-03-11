using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP
{
    //Head
    public class SendInventoryCounting
    {
        public int Series { get; set; }
        //public string DocEntry { get; set; }

        public string CountingDate { get; set; } // Posting Date (YYYY-MM-DD)
        public string CountingTime { get; set; } // 15:37 

        public string CountingType { get; } = "1"; // 1 = Single Counter, 2 = Multiple Counters

        public string CounterType { get; } = "12"; // 12 = User Type, 171 = Employee Type
        //public int CounterCode { get; set; }
        //public string CounterName { get; set; }

        public string Ref2 { get; set; }
        public string Comments { get; set; }

        public List<SendInventoryCountingLine> Line { get; set; }
        public List<SendInventoryCountingBatch> BatchLine { get; set; }
        public List<SendInventoryCountingSerial> SerialLine { get; set; }
    }


    //Line
    public class SendInventoryCountingLine
    {
        //public string BarCode { get; set; }
        public string ItemCode { get; set; }
        public string WhsCode { get; set; }
        public int BinEntry { get; set; }
        public string Freeze { get; } = "N"; // Y/N
        public string UomCode { get; set; } // Required Field if Item has UOM Group
        public long CountedQuantity { get; set; }
        public string Counted { get; } = "Y";
        public string BatchNo { get; set; }
        public string SerialNo { get; set; }
        public long Quantity { get; set; }        
    }

    public class SendInventoryCountingBatch 
    {
        public string BatchNumber { get; set; }
        public double Quantity { get; set; }
    }

    public class SendInventoryCountingSerial
    {
        public string SerialNumber { get; set; }
        public double Quantity { get; set; }
    }




}

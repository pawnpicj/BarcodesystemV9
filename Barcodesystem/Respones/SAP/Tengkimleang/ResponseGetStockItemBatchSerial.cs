using System;
using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetStockItemBatchSerial
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetStockItemBatchSerial> Data { get; set; }
    }

    public class GetStockItemBatchSerial
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public string UOMCode { get; set; }
        public int WhsEntry { get; set; }
        public string WhsCode { get; set; }
        public int BinEntry { get; set; }
        public string BinCode { get; set; }
        public string BatchNumber { get; set; }
        public string SerialNumber { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
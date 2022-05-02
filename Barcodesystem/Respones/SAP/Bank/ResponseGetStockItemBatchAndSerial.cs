using System;
using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class ResponseGetStockItemBatchAndSerial
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetStockItemBatchAndSerial> Data { get; set; }
    }

    public class GetStockItemBatchAndSerial
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
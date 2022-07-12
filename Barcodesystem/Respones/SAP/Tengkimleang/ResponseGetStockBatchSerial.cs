using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetStockBatchSerial
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetStockBatchSerial> Data { get; set; }
    }

    public class GetStockBatchSerial
    {
        public string ItemCode { get; set; }
        public double OnHand { get; set; }
        public string SerialNumber { get; set; }
        public string BatchNumber { get; set; }
        public string ItemName { get; set; }
        public string UomCode { get; set; }
    }
}
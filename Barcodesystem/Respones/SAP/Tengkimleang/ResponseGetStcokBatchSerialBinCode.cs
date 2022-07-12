using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetStcokBatchSerialBinCode
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetStockBatchSerialBinCode> Data { get; set; }
    }

    public class GetStockBatchSerialBinCode
    {
        public string ItemCode { get; set; }
        public double OnHand { get; set; }
        public string SerialNumber { get; set; }
        public string BatchNumber { get; set; }
    }
}
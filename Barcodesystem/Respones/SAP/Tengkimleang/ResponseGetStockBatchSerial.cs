using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string SerailNumber { get; set; }
        public string BatchNumber { get; set; }
    }
}

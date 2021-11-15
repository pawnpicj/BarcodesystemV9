using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetStockBatchSerialWarehouse
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetStockBatchSerialWarehouse> Data { get; set; }
    }
    public class GetStockBatchSerialWarehouse
    {
        public string ItemCode { get; set; }
        public double OnHand { get; set; }
        public string SerailNumber { get; set; }
        public string BatchNumber { get; set; }
    }
}

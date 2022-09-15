using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Vichika
{
    public class ResponseGetBatch
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetBatch> Data { get; set; }
    }

    public class GetBatch
    {
        public string ItemCode { get; set; }
        public string BatchNumber { get; set; }
        public double Qty { get; set; }
        public string ExpDate { get; set; }
        public double InputQty { get; set; }
    }
}

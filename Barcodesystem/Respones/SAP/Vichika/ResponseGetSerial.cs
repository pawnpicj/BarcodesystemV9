using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Vichika
{
    public class ResponseGetSerial
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetSerial> Data { get; set; }
    }

    public class GetSerial
    {
        public string ItemCode { get; set; }
        public string SerialNumber { get; set; }
        public double Qty { get; set; }
    }
}

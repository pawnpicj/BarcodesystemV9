using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOSRIGetSerial
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OSRI> Data { get; set; }
    }
    public class OSRI
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string IntrSerial { get; set; }
        public string WhsCode { get; set; }
        public int Quantity { get; set; }
    }
}

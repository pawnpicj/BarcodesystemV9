using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOBTNGetBatch
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OBTN> Data { get; set; }
    }
    public class OBTN { 
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string BatchNumber { get; set; }
        public string ExpDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOSLPGetSalesEmployee
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OSLP> Data { get; set; }
    }
    public class OSLP { 
        public int SlpCode { get; set; }
        public string SlpName { get; set; }
    }
}

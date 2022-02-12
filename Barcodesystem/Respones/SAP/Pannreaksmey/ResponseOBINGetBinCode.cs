using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOBINGetBinCode
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OBIN> Data { get; set; }
    }
    public class OBIN
    {
        public string BinCode { get; set; }
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
    }
}

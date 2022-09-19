using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class ResponseGetOUOM
    {
        public int ErrorCode { get; set; }
        public string? ErrorMsg { get; set; }
        public List<OUOMList> Data { get; set; }
    }
    public class OUOMList
    {
        public string UomCode { get; set; }
        public string UomName { get; set; }
    }
}


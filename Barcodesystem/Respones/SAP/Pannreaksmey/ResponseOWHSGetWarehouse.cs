using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOWHSGetWarehouse
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OWHS> Data { get; set; }
    }
    public class OWHS
    {
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Pannreaksmey
{
    public class ResponseGBL1BinLocationGenerate
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<GBL1> Data { get; set; }
    }
    public class GBL1{
        public string GNumber { get; set; }
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public string BinCode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOPRCGetCostCenter
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OPRC> Data { get; set; }
    }
    public class OPRC
    {
        public int PrcCode { get; set; }
        public string PrcName { get; set; }
    }
}

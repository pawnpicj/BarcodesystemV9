using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOUSRGetUser
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OUSR> Data { get; set; }
    }
       public class OUSR
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
    }
}

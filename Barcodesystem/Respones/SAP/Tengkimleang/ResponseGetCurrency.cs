using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetCurrency
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetCurrency> Data { get; set; }
    }
    public class GetCurrency
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

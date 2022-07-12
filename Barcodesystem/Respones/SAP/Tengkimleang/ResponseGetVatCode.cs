using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetVatCode
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetVatCode> Data { get; set; }
    }

    public class GetVatCode
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
    }
}

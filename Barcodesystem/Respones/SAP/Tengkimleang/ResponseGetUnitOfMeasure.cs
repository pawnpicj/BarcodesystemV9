using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetUnitOfMeasure
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetUnitOfMeasure> Data { get; set; }
    }

    public class GetUnitOfMeasure
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

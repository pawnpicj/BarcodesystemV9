using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP.Tengkimleang
{
    public class GetGenerateSerialBatchRequest
    {
        public string itemCode { get; set; }
        public string qty { get; set; }
    }
}

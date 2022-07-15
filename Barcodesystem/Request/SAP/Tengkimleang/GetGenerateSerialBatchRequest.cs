using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP.Tengkimleang
{
    public class GetGenerateSerialBatchRequest
    {
        public List<SerialGen> ListSerials { get; set; }

    }
    public class SerialGen
    {
        public string itemCode { get; set; }
        public string qty { get; set; }
        public int SerialFrom { get; set; }
        public int SerialTo { get; set; }
        public string TypeSerialGen { get; set; }
        public string MfrNo { get; set; }
        public string ExpireDate { get; set; }
    }
}

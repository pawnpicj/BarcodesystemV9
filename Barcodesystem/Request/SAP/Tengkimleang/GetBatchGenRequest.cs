using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP.Tengkimleang
{
    public class GetBatchGenRequest
    {
        public List<Batches> ListBatches { get; set; }
    }

    public class Batches
    {
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int BatchFrom { get; set; }
        public int BatchTo { get; set; }
        public string ItemCode { get; set; }
        public int Qty { get; set; }
        public int BatchCount { get; set; }
        public string TypeBatchGen { get; set; }
    }
}

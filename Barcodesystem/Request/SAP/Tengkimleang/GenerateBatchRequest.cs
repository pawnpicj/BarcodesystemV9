using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP.Tengkimleang
{
    public class GenerateBatchRequest
    {
        public List<Batch> ListBatch { get; set; }
        public Batch Batch { get; set; }
    }
    public class Batch
    {
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int BatchFrom { get; set; }
        public int BatchTo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public  class ResponseNNM1GetDocumentSeries
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<NNM1> Data { get; set; }
    }
    public class NNM1
    {
        public int ObjectCode { get; set; }
        public int Series { get; set; }
        public string SeriesName { get; set; }
        public string InitialNum { get; set; }
        public string NextNumber { get; set; }
        public string Indicator { get; set; }
    }
}

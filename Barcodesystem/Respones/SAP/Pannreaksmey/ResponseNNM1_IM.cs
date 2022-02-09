using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseNNM1_IM
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<NNM1IM> Data { get; set; }
    }

    public class NNM1IM
    {
        public string ObjectCode { get; set; }
        public int Series { get; set; }
        public string SeriesName { get; set; }
        public string Indicator { get; set; }
        public string BeginStr { get; set; }
    }
}

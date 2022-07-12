using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseNNM1GetDocumentSeries
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<NNM1> Data { get; set; }
    }

    public class NNM1
    {
        public string ObjectCode { get; set; }
        public int Series { get; set; }
        public string SeriesName { get; set; }
        public int InitialNum { get; set; }
        public int NextNumber { get; set; }
        public string Indicator { get; set; }
    }
}
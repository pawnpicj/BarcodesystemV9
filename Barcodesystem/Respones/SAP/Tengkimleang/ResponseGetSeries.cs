using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetSeries
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetSeries> Data { get; set; }
    }

    public class GetSeries
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
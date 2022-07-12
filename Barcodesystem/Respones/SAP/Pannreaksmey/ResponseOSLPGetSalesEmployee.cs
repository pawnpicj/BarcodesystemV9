using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOSLPGetSalesEmployee
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OSLP> Data { get; set; }
    }

    public class OSLP
    {
        public int SlpCode { get; set; }
        public string SlpName { get; set; }
    }
}
using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetGenerateBatchSerial
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<GetGenerateBatchSerial> Data { get; set; }
    }

    public class GetGenerateBatchSerial
    {
        public string SerialAndBatch { get; set; }
    }
}
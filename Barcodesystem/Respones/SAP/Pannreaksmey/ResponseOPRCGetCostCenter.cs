using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOPRCGetCostCenter
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OPRC> Data { get; set; }
    }

    public class OPRC
    {
        public string PrcCode { get; set; }
        public string PrcName { get; set; }
    }
}
using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOUSRGetUser
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OUSR> Data { get; set; }
    }

    public class OUSR
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
    }
}
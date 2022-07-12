using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOACTGetGLAccount
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OACT> Data { get; set; }
    }

    public class OACT
    {
        public string AcctCode { get; set; }
        public string AcctName { get; set; }
    }
}
using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Pannreaksmey
{
    public class ResponseGetBinCodeGeneration
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OGBC> Data { get; set; }
    }

    public class OGBC
    {
        public string GID { get; set; }
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public string BinCode { get; set; }
    }
}
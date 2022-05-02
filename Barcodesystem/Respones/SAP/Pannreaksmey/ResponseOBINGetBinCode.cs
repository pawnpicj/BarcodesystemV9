using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOBINGetBinCode
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OBIN> Data { get; set; }
    }

    public class OBIN
    {
        public string BinCode { get; set; }
        public string Descr { get; set; }
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public int AbsEntry { get; set; }
    }
}
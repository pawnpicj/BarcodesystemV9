using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseGetBinLocationList
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<lBINl> Data { get; set; }
    }

    public class lBINl
    {
        public string ItemCode { get; set; }
        public string WhsCode { get; set; }
        public string BinCode { get; set; }
        public string Qty { get; set; }
        public string BarCode { get; set; }
    }
}
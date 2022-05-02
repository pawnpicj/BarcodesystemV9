using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOWHSGetWarehouse
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OWHS> Data { get; set; }
    }

    public class OWHS
    {
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
    }
}
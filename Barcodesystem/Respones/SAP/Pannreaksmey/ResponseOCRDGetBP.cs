using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOCRDGetBP
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OCRD> Data { get; set; }
    }

    public class OCRD
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string CardFName { get; set; }
        public string CardType { get; set; }
        public string GroupName { get; set; }
        public string Phone { get; set; }
        public string LicTradNum { get; set; }
    }
}
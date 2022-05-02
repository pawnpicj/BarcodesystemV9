using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseCRD1Address
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<CRD1> Data { get; set; }
    }

    public class CRD1
    {
        public string AdreType { get; set; }
        public string Address { get; set; }
        public string CardCode { get; set; }
        public string Street { get; set; }
        public string Block { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
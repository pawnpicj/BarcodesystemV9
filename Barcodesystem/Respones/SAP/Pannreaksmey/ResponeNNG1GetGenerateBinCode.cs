using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Pannreaksmey
{
    public class ResponeNNG1GetGenerateBinCode
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<NNG1> Data { get; set; }
    }

    public class NNG1
    {
        public string SeriesID { get; set; }
    }
}
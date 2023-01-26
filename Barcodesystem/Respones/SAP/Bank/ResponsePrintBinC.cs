using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class ResponsePrintBinC
    {
        public List<lData> Data { get; set; }
    }

    public static class PrintBinCStatic
    {
        public static List<lData> Data { get; set; }
    }

    public class lData
    {
        public string whsCode { get; set; }
        public string whsName { get; set; }
        public string binCode { get; set; }
        public string c_BinCode { get; set; }
    }
}
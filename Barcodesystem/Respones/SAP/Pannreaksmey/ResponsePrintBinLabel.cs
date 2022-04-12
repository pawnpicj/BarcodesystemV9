using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Pannreaksmey
{
    public class ResponsePrintBinLabel
    {
        public List<OBINLabel> Data { get; set; }   
    }
    public static class ResponsePrintLabelStatic
    {
       public static List<OBINLabel> Data { get; set; }
    }
    public class OBINLabel
    {
        public string Code { get; set; }
       public string WhsCode { get; set; }
       public string BinCode { get; set; }
       public string Descr { get; set; }
    }
}

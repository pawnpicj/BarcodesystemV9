using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseGetBinLocation
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<lBIN> Data { get; set; }
    }
    public class lBIN
    {
        public string BinCode { get; set; }
        public string Descr { get; set; }
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public int AbsEntry { get; set; }
        
    }
}

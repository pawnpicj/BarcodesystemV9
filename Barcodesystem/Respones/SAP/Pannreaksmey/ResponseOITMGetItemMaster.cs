using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOITMGetItemMaster
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OITM> Data { get; set; }

    }
    public class OITM
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemFName { get; set; }
        public string ItemGroup { get; set; }
        public string ManBtchNum { get; set; }
        public string ManSerNum { get; set; }
        public string UoM { get; set; }
    }
}

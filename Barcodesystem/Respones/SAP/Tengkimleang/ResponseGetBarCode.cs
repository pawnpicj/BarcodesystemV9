using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetBarCode
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<OBCD> Data { get; set; }
    }

    public class OBCD
    {
        public string BarCode { get; set; }
        public string BarCodeName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UomCode { get; set; }
        public double Price { get; set; }
        public string UomName { get; set; }
        public string ManageItem { get; set; }
    }
}

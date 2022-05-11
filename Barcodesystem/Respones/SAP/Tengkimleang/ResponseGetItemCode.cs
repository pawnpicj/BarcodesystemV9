using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetItemCode
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetItemCode> Data { get; set; }
    }

    public class GetItemCode
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string BarCode { get; set; }
        public string UomName { get; set; }
        public string ManageItem { get; set; }
    }
}

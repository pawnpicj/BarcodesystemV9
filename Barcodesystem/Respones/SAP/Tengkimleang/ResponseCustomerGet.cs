using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseCustomerGet
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<CustomerGet> Data { get; set; }
    }
    public class CustomerGet
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}

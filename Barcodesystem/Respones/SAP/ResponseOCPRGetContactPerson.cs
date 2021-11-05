using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseOCPRGetContactPerson
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<OCPR> Data { get; set; }
    }
    public class OCPR {
        //CardCode,Name,Position,Address,Tel1,Tel2,Cellolar
        public string CardCode { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Cellolar { get; set; }
    }
}

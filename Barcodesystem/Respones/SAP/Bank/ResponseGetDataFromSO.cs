using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class ResponseGetDataFromSO
    {
        public List<lSO> Data { get; set; }
    }
    public static class SOStatic
    {
        public static List<lSO> Data { get; set; }
    }
    public class lSO
    {
        public int Docentry { get; set; }        
    }
}

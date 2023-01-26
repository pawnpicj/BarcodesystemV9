using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class SetupTypeModel
    {
        public string type { get; set; }
        public string desc { get; set; }
        public string dateCounting { get; set; }
        public string timeCounting { get; set; }
        public string active { get; set; }
    }
}

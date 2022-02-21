using BarCodeLibrary.Respones.SAP.Pannreaksmey;
using BarCodeLibrary.Request.SAP.Pannreaksmey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP.Pannreaksmey
{
    public class SendBinLocationGenerate
    {
        public string GID   { get; set; }
        public string WhsCode { get; set; }
        public string  WhsName { get; set; }
        public string BinCode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Pannreaksmey
{
    public class ResponseDepartment
    {
       public List<DEP> Data { get; set; }
    }
    public class DEP
    {
        public string Department { get; set; }
    }
}

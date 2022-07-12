using System;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service.Pannreaksmey
{
    public interface IDepartmentService
    {
        Task<ResponseDepartment> ResponseDepartment();
        Task<ResponseDepartment> ResponsePostDepartment(DEP dep);
    }
}

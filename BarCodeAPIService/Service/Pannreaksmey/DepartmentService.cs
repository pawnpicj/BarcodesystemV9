using System;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeAPIService.Models;
using System.Data;

namespace BarCodeAPIService.Service.Pannreaksmey
{
    public class DepartmentService:IDepartmentService
    {
        public Task<ResponseDepartment> ResponseDepartment()
        {
            var clsCRUD = new ClsCRUD();
            var Dept = new List<DEP>();
            var dt = clsCRUD.GetDataWeb("SELECT DEPARTMENT FROM \"" + ConnectionString.BarcodeDb+ "\".TBDEP", "WebDb");
            var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SqlHana);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Dept.Add(new DEP
                        {
                            Department = row[0].ToString()
                        });
                    }
                    catch (Exception ex)
                    {
                        var e1 = ex.Message;
                    }
 
                }
                return Task.FromResult(new ResponseDepartment { Data = Dept.ToList() });
            }
            else
            {
                return Task.FromResult(new ResponseDepartment { Data = null });
            }          
        }
        public Task<ResponseDepartment> ResponsePostDepartment(DEP dep)
        {
            var clsCRUD = new ClsCRUD();
            var LstDept = new List<ResponseDepartment>();
            var dt = clsCRUD.GetDataWeb("INSERT INTO \"" + ConnectionString.BarcodeDb +
                "\".TBDEP(DEPARTMENT) VALUES('" + dep.Department + "')", "WebDb");
            if (dt != null)
            {
                return Task.FromResult(new ResponseDepartment
                {
                    Data = null
                }) ;
            }
            else
            {
                return Task.FromResult(new ResponseDepartment
                {
                    Data = null
                }) ;
            }
        }
    }
}

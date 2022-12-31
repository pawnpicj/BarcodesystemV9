using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeAPIService.Models;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public class SaleEmployeeService : ISaleEmployeeService
    {
        public Task<ResponseOSLPGetSalesEmployee> ResponseOSLPGetSalesEmployee()
        {
            var oSLP = new List<OSLP>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query = "CALL \"" + ConnectionString.CompanyDB +
                                "\"._USP_CALLTRANS_Smey ('OSLP','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        oSLP.Add(new OSLP
                        {
                            SlpCode = Convert.ToInt32(row[0].ToString()),
                            SlpName = row[1].ToString(),
                            SlpId = row[2].ToString()                            
                        });
                    return Task.FromResult(new ResponseOSLPGetSalesEmployee
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oSLP.ToList()
                    });
                }

                return Task.FromResult(new ResponseOSLPGetSalesEmployee
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOSLPGetSalesEmployee
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}
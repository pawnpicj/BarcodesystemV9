using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using BarCodeAPIService.Models;

namespace BarCodeAPIService.Service
{
    public class SaleEmployeeService : ISaleEmployeeService
    {
        public Task<ResponseOSLPGetSalesEmployee> ResponseOSLPGetSalesEmployee()
        {
            var oSLP = new List<OSLP>();
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OSLP','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oSLP.Add(new OSLP
                        {
                            SlpCode=Convert.ToInt32(row[0].ToString()),
                            SlpName=row[1].ToString()
                        });
                    }                   
                    return Task.FromResult(new ResponseOSLPGetSalesEmployee
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oSLP.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOSLPGetSalesEmployee
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
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

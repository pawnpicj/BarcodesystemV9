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
    public class GLAccountService : IGLAccountService
    {
        public Task<ResponseOACTGetGLAccount> ResponseOACTGetGLAccount()
        {
            var oACT = new List<OACT>();
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OACT','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oACT.Add(new OACT
                        {
                            AcctCode=row[0].ToString(),
                            AcctName=row[1].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseOACTGetGLAccount
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oACT.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOACTGetGLAccount
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOACTGetGLAccount
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

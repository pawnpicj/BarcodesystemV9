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
    public class GLAccountService : IGLAccountService
    {
        public Task<ResponseOACTGetGLAccount> ResponseOACTGetGLAccount()
        {
            var oACT = new List<OACT>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query = "CALL \"" + ConnectionString.CompanyDB +
                                "\"._USP_CALLTRANS_Smey ('OACT','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        oACT.Add(new OACT
                        {
                            AcctCode = row[0].ToString(),
                            AcctName = row[1].ToString()
                        });
                    return Task.FromResult(new ResponseOACTGetGLAccount
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oACT.ToList()
                    });
                }

                return Task.FromResult(new ResponseOACTGetGLAccount
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
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
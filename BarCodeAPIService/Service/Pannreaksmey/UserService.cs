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
    public class UserService : IUserService
    {
        public Task<ResponseOUSRGetUser> ResponseOUSRGetUser()
        {
            var oUSR = new List<OUSR>();
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    String Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OUSR','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oUSR.Add(new OUSR
                        {
                            UserCode=row[0].ToString(),
                            UserName=row[1].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseOUSRGetUser
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oUSR.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOUSRGetUser
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOUSRGetUser
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using BarCodeAPIService.Models;

namespace BarCodeAPIService.Service.Pannreaksmey
{
    public class GenerateBinCodeService : IGenerateBinCodeServices
    {
        public Task<ResponeNNG1GetGenerateBinCode> ResponeNNG1GetGenerateBinCode()
        {
            var nNG1 = new List<NNG1>();
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query= "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('NNG1','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        nNG1.Add(new NNG1 { 
                            SeriesID=row[0].ToString(),
                        });
                    }
                    return Task.FromResult(new ResponeNNG1GetGenerateBinCode { 
                        ErrorCode=0,
                        ErrorMessage="",
                        Data=nNG1.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponeNNG1GetGenerateBinCode { 
                        ErrorCode=login.lErrCode,
                        ErrorMessage=login.sErrMsg,
                        Data =null
                    });
                }
            }
            catch (Exception ex) {
                return Task.FromResult(new ResponeNNG1GetGenerateBinCode
                {
                    ErrorCode=ex.HResult,
                    ErrorMessage=ex.Message,
                    Data=null
                });
            }
        }
    }
}

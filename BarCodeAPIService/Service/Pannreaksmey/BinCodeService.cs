using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using BarCodeAPIService.Models;

namespace BarCodeAPIService.Service
{
   
    public class BinCodeService : IBinCodeService
    {

        public Task<ResponseOBINGetBinCode> ResponseOBINGetBinCode()
        {
            var oBIN = new List<OBIN>();
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {                    
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OBIN','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oBIN.Add(new OBIN { 
                            BinCode=row[0].ToString(),
                            Descr=row[1].ToString(),
                            WhsCode=row[2].ToString(),
                            WhsName=row[3].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseOBINGetBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oBIN.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOBINGetBinCode
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOBINGetBinCode
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

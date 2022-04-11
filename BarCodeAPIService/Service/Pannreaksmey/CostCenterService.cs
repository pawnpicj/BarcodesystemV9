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
    public class CostCenterService : ICostCenterService
    {
        public Task<ResponseOPRCGetCostCenter> ResponseOPRCGetCostCenter()
        {
            var oPRC = new List<OPRC>();
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OPRC','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oPRC.Add(new OPRC
                        {
                            PrcCode=row[0].ToString(),
                            PrcName=row[1].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseOPRCGetCostCenter
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oPRC.ToList()
                    });
                }
                else {
                    return Task.FromResult(new ResponseOPRCGetCostCenter { 
                        ErrorCode=login.lErrCode,
                        ErrorMessage=login.sErrMsg,
                        Data=null
                    });
                }                
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOPRCGetCostCenter { 
                    ErrorCode=ex.HResult,
                    ErrorMessage=ex.Message,
                    Data=null
                });
            }
        }
    }
}

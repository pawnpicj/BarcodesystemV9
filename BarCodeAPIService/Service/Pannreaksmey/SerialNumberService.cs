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
    public class SerialNumberService : ISerialNumberService
    {
        public Task<ResponseOSRIGetSerial> ResponseOSRIGetSerial()
        {
            var oSRI = new List<OSRI>();
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OSRI','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oSRI.Add(new OSRI
                        {
                            ItemCode=row[0].ToString(),
                            ItemName=row[1].ToString(),
                            IntrSerial=row[2].ToString(),
                            ExpDate=row[3].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseOSRIGetSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oSRI.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOSRIGetSerial
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOSRIGetSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

        }
    }
}

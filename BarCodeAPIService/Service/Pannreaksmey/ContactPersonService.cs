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
    public class ContactPersonService : IContactPersonService
    {
        public Task<ResponseOCPRGetContactPerson> ResponseOCPRGetContactPerson()
        {
            var oCPR = new List<OCPR>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query = "CALL \"" + ConnectionString.CompanyDB +
                                "\"._USP_CALLTRANS_Smey ('OCPR','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        oCPR.Add(new OCPR
                        {
                            CardCode = row[0].ToString(),
                            Name = row[1].ToString(),
                            Position = row[2].ToString(),
                            Address = row[3].ToString(),
                            Tel1 = row[4].ToString(),
                            Tel2 = row[5].ToString(),
                            Cellolar = row[6].ToString()
                        });
                    return Task.FromResult(new ResponseOCPRGetContactPerson
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oCPR.ToList()
                    });
                }

                return Task.FromResult(new ResponseOCPRGetContactPerson
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOCPRGetContactPerson
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}
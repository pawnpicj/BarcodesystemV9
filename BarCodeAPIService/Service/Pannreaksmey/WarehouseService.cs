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
    public class WarehouseService : IWarehouseService
    {
        public Task<ResponseOWHSGetWarehouse> responseWHSGetWarehouse()
        {
            var oWHS = new List<OWHS>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    var Query = "CALL \"" + ConnectionString.CompanyDB +
                                "\"._USP_CALLTRANS_Smey ('OWHS','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        oWHS.Add(new OWHS
                        {
                            WhsCode = row[0].ToString(),
                            WhsName = row[1].ToString()
                        });
                    return Task.FromResult(new ResponseOWHSGetWarehouse
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oWHS.ToList()
                    });
                    ;
                }

                return Task.FromResult(new ResponseOWHSGetWarehouse
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOWHSGetWarehouse
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}
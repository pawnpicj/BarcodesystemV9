using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using BarCodeAPIService.Models;
using SAPbobsCOM;
using System.Globalization;
using System.Data.Odbc;


namespace BarCodeAPIService.Service
{
    public class WarehouseService : IWarehouseService
    {

        public Task<ResponseOWHSGetWarehouse> responseWHSGetWarehouse()
        {


            var dataLine = new List<OWHS>();
            var dt = new DataTable();

            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {

                    var Query = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_Smey ('OWHS','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        dataLine.Add(new OWHS
                        {

                            WhsCode = row["WhsCode"].ToString(),
                            WhsName = row["WhsName"].ToString(),

                        });
                    }
                    return Task.FromResult(new ResponseOWHSGetWarehouse
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = dataLine.ToList()
                    });
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

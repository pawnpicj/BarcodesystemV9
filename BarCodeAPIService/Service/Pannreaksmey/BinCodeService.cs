using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using SAPbobsCOM;
using BarCodeAPIService.Models;
using System.Globalization;
using System.Data;
using System.Data.Odbc;

namespace BarCodeAPIService.Service
{
    public class BinCodeService : IBinCodeService
    {
        public Task<ResponseOBINGetBinCode> ResponseGetBinCodeByCode(string binCode)
        {
            var oLine = new List<OBIN>();
            var dt = new DataTable();
            var dtLine = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_Smey('OBinByCode','{binCode}','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oLine.Add(new OBIN
                        {
                            BinCode = row["BinCode"].ToString(),
                            Descr = row["Descr"].ToString(),
                            WhsCode = row["WhsCode"].ToString(),
                            WhsName = row["WhsName"].ToString(),
                            AbsEntry = Convert.ToInt32(row["AbsEntry"].ToString())
                        });
                    }
                    return Task.FromResult(new ResponseOBINGetBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oLine.ToList()
                    });
                }
                return Task.FromResult(new ResponseOBINGetBinCode
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });

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

        public Task<ResponseOBINGetBinCode> ResponseOBINGetBinCode()
        {
            var oLine = new List<OBIN>();
            var dt = new DataTable();
            var dtLine = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_Smey('OBIN','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oLine.Add(new OBIN
                        {
                            BinCode = row["BinCode"].ToString(),
                            Descr = row["Descr"].ToString(),
                            WhsCode = row["WhsCode"].ToString(),
                            WhsName = row["WhsName"].ToString(),
                            AbsEntry = Convert.ToInt32(row["AbsEntry"].ToString())
                        });
                    }
                    return Task.FromResult(new ResponseOBINGetBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oLine.ToList()
                    });
                }
                return Task.FromResult(new ResponseOBINGetBinCode
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });

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
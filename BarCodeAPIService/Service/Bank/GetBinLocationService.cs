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

namespace BarCodeAPIService.Service.Bank
{
    public class GetBinLocationService : IGetBinLocationService
    {
        public Task<ResponseGetBinLocation> responseGetBinLocation(string whscode)
        {
            var oBIN = new List<lBIN>();
            var dt = new DataTable();

            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {

                    var Query = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('GetBinLocationWhs','{whscode}','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oBIN.Add(new lBIN
                        {
                            BinCode = row["binCode"].ToString(),
                            Descr = row["descr"].ToString(),
                            WhsCode = row["whsCode"].ToString(),
                            WhsName = row["whsName"].ToString(),
                            AbsEntry = Convert.ToInt32(row["absEntry"].ToString())
                        });
                    }
                    return Task.FromResult(new ResponseGetBinLocation
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oBIN.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetBinLocation
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetBinLocation
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetBinLocation> responseGetBinLocationCounting(string whscode, string iyear)
        {
            var oBIN = new List<lBIN>();
            var dt = new DataTable();

            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {

                    var Query = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK('RptBinLocationCounting','{whscode}','{iyear}','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oBIN.Add(new lBIN
                        {
                            BinCode = row["binCode"].ToString(),
                            Descr = row["descr"].ToString(),
                            WhsCode = row["whsCode"].ToString(),
                            WhsName = row["whsName"].ToString(),
                            AbsEntry = Convert.ToInt32(row["absEntry"].ToString()),
                            C_BinCode = row["C_BinCode"].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseGetBinLocation
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oBIN.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetBinLocation
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetBinLocation
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }


    }
}
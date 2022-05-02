using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeAPIService.Models;
using BarCodeLibrary.Request.SAP.Pannreaksmey;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;

namespace BarCodeAPIService.Service.Pannreaksmey
{
    public class GenerateBinCodeService : IGenerateBinCodeServices
    {
        public Task<ResponeNNG1GetGenerateBinCode> ResponeNNG1GetGenerateBinCode()
        {
            var nNG1 = new List<NNG1>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    var Query = "CALL \"" + ConnectionString.CompanyDB +
                                "\"._USP_CALLTRANS_Smey ('NNG1','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        nNG1.Add(new NNG1
                        {
                            SeriesID = row[0].ToString()
                        });
                    return Task.FromResult(new ResponeNNG1GetGenerateBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = nNG1.ToList()
                    });
                }

                return Task.FromResult(new ResponeNNG1GetGenerateBinCode
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponeNNG1GetGenerateBinCode
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        Task<ResponseGetBinCodeGeneration> IGenerateBinCodeServices.PostGenerationCode(
            SendBinLocationGenerate sendBinlocation)
        {
            var oGBC = new List<OGBC>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    var Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OGBC',\"" +
                                sendBinlocation.GID + "\",\"" + sendBinlocation.WhsCode + "\",'\"" +
                                sendBinlocation.WhsName + "\"',\"" + sendBinlocation.BinCode + "\",'')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        oGBC.Add(new OGBC
                        {
                            GID = row[0].ToString(),
                            WhsCode = row[1].ToString(),
                            WhsName = row[2].ToString(),
                            BinCode = row[3].ToString()
                        });
                    return Task.FromResult(new ResponseGetBinCodeGeneration
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oGBC.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetBinCodeGeneration
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetBinCodeGeneration
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        Task<ResponseGetBinCodeGeneration> IGenerateBinCodeServices.ResponseGetBinGeneration()
        {
            var oGBC = new List<OGBC>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    var Query = "CALL \"" + ConnectionString.CompanyDB +
                                "\"._USP_CALLTRANS_Smey ('OGBC','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        oGBC.Add(new OGBC
                        {
                            GID = row[0].ToString(),
                            WhsCode = row[1].ToString(),
                            WhsName = row[2].ToString(),
                            BinCode = row[3].ToString()
                        });
                    return Task.FromResult(new ResponseGetBinCodeGeneration
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oGBC.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetBinCodeGeneration
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetBinCodeGeneration
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}
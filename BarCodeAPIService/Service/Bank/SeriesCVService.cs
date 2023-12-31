﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP.Bank;
using SAPbobsCOM;
using BarCodeLibrary.Respones.SAP;
using BarCodeAPIService.Models;
using System.Globalization;
using System.Data;
using System.Data.Odbc;

namespace BarCodeAPIService.Service.Bank
{
    public class SeriesCVService : ISeriesCVService
    {
        public Task<ResponseNNM1_CV> responseNNM1_CV()
        {
            var oSCV = new List<NNM1CV>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset oRS = null;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    var Query = "CALL \"" + ConnectionString.CompanyDB +
                                "\"._USP_CALLTRANS_BANK ('NNM1CV','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        oSCV.Add(new NNM1CV
                        {
                            ObjectCode = oRS.Fields.Item(0).Value.ToString(),
                            Series = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SeriesName = oRS.Fields.Item(2).Value.ToString(),
                            Indicator = oRS.Fields.Item(3).Value.ToString(),
                            BeginStr = oRS.Fields.Item(4).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseNNM1_CV
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oSCV.ToList()
                    });
                }

                return Task.FromResult(new ResponseNNM1_CV
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseNNM1_CV
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
        
        public Task<ResponseGetSeriesCode> responseGetSeriesCode(string yymm, string typeSeries)
        {
            var oLineSeries = new List<GetSeriesCode>();
            var dt = new DataTable();
            var dtLine = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if(login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('StrSeries','{yymm}','{typeSeries}','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oLineSeries.Add(new GetSeriesCode
                        {
                            ObjectCode = row["ObjectCode"].ToString(),
                            Series = Convert.ToInt32(row["Series"].ToString()),
                            SeriesName = row["SeriesName"].ToString(),
                            Indicator = row["Indicator"].ToString(),
                            BeginStr = row["BeginStr"].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseGetSeriesCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oLineSeries.ToList()
                    });
                }
                return Task.FromResult(new ResponseGetSeriesCode
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });

            }
            catch (Exception ex)
            {

                return Task.FromResult(new ResponseGetSeriesCode
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            
            
        }

    }
}
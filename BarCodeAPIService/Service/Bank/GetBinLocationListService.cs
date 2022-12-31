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
    public class GetBinLocationListService : IGetBinLocationListService
    {
        public Task<ResponseGetBinLocationList> responseGetBinLocationList(string barcode, string itemcode)
        {

            var dataLine = new List<lBINl>();
            var dt = new DataTable();

            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {

                    if ((barcode != null || barcode != "") && barcode != "empty")
                    {
                        //==================================== 
                        var Query = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('BinLocationListForBatch','{barcode}','{itemcode}','','','')";
                        login.AD = new OdbcDataAdapter(Query, login.CN);
                        login.AD.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {

                            foreach (DataRow row in dt.Rows)
                            {
                                dataLine.Add(new lBINl
                                {

                                    ItemCode = row["ItemCode"].ToString(),
                                    WhsCode = row["WhsCode"].ToString(),
                                    BinCode = row["BinCode"].ToString(),
                                    Qty = row["Qty"].ToString(),
                                    BarCode = row["BarCode"].ToString()

                                });
                            }

                        }
                        else
                        {
                            var Query2 = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('BinLocationListForSerial','{barcode}','{itemcode}','','','')";
                            login.AD = new OdbcDataAdapter(Query2, login.CN);
                            login.AD.Fill(dt);

                            foreach (DataRow row in dt.Rows)
                            {
                                dataLine.Add(new lBINl
                                {

                                    ItemCode = row["ItemCode"].ToString(),
                                    WhsCode = row["WhsCode"].ToString(),
                                    BinCode = row["BinCode"].ToString(),
                                    Qty = row["Qty"].ToString(),
                                    BarCode = row["BarCode"].ToString()

                                });
                            }

                        }
                        //==================================== 
                    }
                    else
                    {
                        //Bin Location List for No Batch Number and Serial Number
                        var Query3 = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('BinLocationListNoBatchSerial','','{itemcode}','','','')";
                        login.AD = new OdbcDataAdapter(Query3, login.CN);
                        login.AD.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            dataLine.Add(new lBINl
                            {

                                ItemCode = row["ItemCode"].ToString(),
                                WhsCode = row["WhsCode"].ToString(),
                                BinCode = row["BinCode"].ToString(),
                                Qty = row["Qty"].ToString(),
                                BarCode = row["BarCode"].ToString()

                            });
                        }

                    }
                    //==== END ====

                    return Task.FromResult(new ResponseGetBinLocationList
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = dataLine.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetBinLocationList
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetBinLocationList
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

        }
    }
}
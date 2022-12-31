using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using BarCodeAPIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Data;
using System.Data.Odbc;
using SAPbobsCOM;


namespace BarCodeAPIService.Service
{
    public class InventoryTransferIMService : IInventoryTransferIMService
    {
        public Task<ResponseGetOWTR> responseGetOWTR()
        {

            var dataLine = new List<OWTR>();
            var dt = new DataTable();

            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {

                    var Query = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('OWTR-IM','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        dataLine.Add(new OWTR
                        {

                            DocNum = row["DocNum"].ToString(),
                            DocEntry = row["DocEntry"].ToString(),
                            DocDate = row["DocDate"].ToString(),
                            CardCode = row["CardCode"].ToString(),
                            CardName = row["CardName"].ToString(),
                            SlpCode = row["SlpCode"].ToString(),
                            SlpName = row["SlpName"].ToString(),
                            FromWhs = row["FromWhs"].ToString(),
                            ToWhs = row["ToWhs"].ToString(),
                            SeriesName = row["SeriesName"].ToString(),                            
                            ToBinEntry = row["ToBinEntry"].ToString(),
                            ToBinCode = row["ToBinCode"].ToString(),
                            LoanNum = row["LoanNum"].ToString()

                        });
                    }
                    return Task.FromResult(new ResponseGetOWTR
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        Data = dataLine.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetOWTR
                {
                    ErrorCode = login.lErrCode,
                    ErrorMsg = login.sErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetOWTR
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetWTRLine> responseGetWTRLine(int DocEntry)
        {
            var dataLine = new List<WTRLine>();
            var dt = new DataTable();

            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('WTR1-IM','{DocEntry}','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        dataLine.Add(new WTRLine
                        {
                            DocEntry = Convert.ToInt32(row["DocEntry"].ToString()),
                            LineNum = Convert.ToInt32(row["BaseLine"].ToString()),
                            ItemCode = row["ItemCode"].ToString(),
                            Dscription = row["Dscription"].ToString(),
                            Quantity = Convert.ToDouble(row["Quantity"].ToString()),
                            U_unitprice = Convert.ToDouble(row["U_unitprice"].ToString()),
                            UomCode = row["UomCode"].ToString(),
                            unitMsr = row["unitMsr"].ToString(),


                            FromWhsCode = row["FromWhs"].ToString(),
                            FromBinCode = row["FromBinCode"].ToString(),
                            FromBinEntry = Convert.ToInt32(row["FromBinEntry"].ToString()),

                            ToWhsCode = row["ToWhs"].ToString(),
                            ToBinCode = row["ToBinCode"].ToString(),
                            ToBinEntry = Convert.ToInt32(row["ToBinEntry"].ToString()),

                            BatchYN = row["BatchYN"].ToString(),
                            SerialYN = row["SerialYN"].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseGetWTRLine
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        Data = dataLine
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseGetWTRLine
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMsg = login.sErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetWTRLine
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message,
                    Data = null
                });
            }

        }

        public Task<ResponseGetIMHeadLine> responseGetIMByCus(string cusCode)
        {
            var oWTRIM = new List<OWTRIM>();
            var wTR1IM = new List<WTR1IM>();
            var dt = new DataTable();
            var dtLine = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('GetIMByCus','{cusCode}','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        dtLine = new DataTable();
                        Query =
                            $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('GetIMLineDocEntry','{row["DocEntry"]}','','','','')";
                        login.AD = new OdbcDataAdapter(Query, login.CN);
                        login.AD.Fill(dtLine);
                        wTR1IM = new List<WTR1IM>();
                        foreach (DataRow drLine in dtLine.Rows)
                            wTR1IM.Add(new WTR1IM
                            {
                                //Line
                                DocEntry = Convert.ToInt32(drLine["DocEntry"].ToString()),
                                LineNum = Convert.ToInt32(drLine["LineNum"].ToString()),
                                ItemCode = drLine["ItemCode"].ToString(),
                                Dscription = drLine["Dscription"].ToString(),
                                Quantity = Convert.ToDouble(drLine["Quantity"].ToString()),
                                Price = Convert.ToDouble(drLine["Price"].ToString()),
                                PriceBefDi = Convert.ToDouble(drLine["PriceBefDi"].ToString()),
                                WhsCode = drLine["WhsCode"].ToString(),
                                LineTotal = Convert.ToDouble(drLine["LineTotal"].ToString()),
                                UomEntry = Convert.ToInt32(drLine["UomEntry"].ToString()),
                                UomCode = drLine["UomCode"].ToString(),
                                FisrtBin = drLine["FisrtBin"].ToString(),
                                IsBtchSerNum = drLine["IsBtchSerNum"].ToString(),
                                BatchSerialNumber = drLine["BatchSerialNumber"].ToString(),
                                BatchSerialQty = Convert.ToDouble(drLine["BatchSerialQty"].ToString()),
                                Patient = drLine["Patient"].ToString()

                            });
                        oWTRIM.Add(new OWTRIM
                        {
                            //Head
                            DocNum = row["DocNum"].ToString(),
                            DocEntry = Convert.ToInt32(row["DocEntry"].ToString()),
                            DocDate = row["DocDate"].ToString(),
                            CardCode = row["CardCode"].ToString(),
                            CardName = row["CardName"].ToString(),
                            SlpCode = row["SlpCode"].ToString(),
                            SlpName = row["SlpName"].ToString(),
                            FromWhs = row["FromWhs"].ToString(),
                            ToWhs = row["ToWhs"].ToString(),
                            SeriesName = row["SeriesName"].ToString(),
                            ToBinEntry = Convert.ToInt32(row["ToBinEntry"].ToString()),
                            ToBinCode = row["ToBinCode"].ToString(),
                            LoanNum = row["LoanNum"].ToString(),
                            Line = wTR1IM.ToList()
                        });
                    }

                    return Task.FromResult(new ResponseGetIMHeadLine
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oWTRIM.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetIMHeadLine
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetIMHeadLine
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

    }
}

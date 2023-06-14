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
    public class InventoryTransferRequestService : IInventoryTransferRequestService
    {
        public Task<ResponseGetOWTQ> responseGetOWTQ()
        {
            var oWTQ = new List<OWTQ>();
            var dt = new DataTable();

            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('OWTQ','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        oWTQ.Add(new OWTQ
                        {
                            DocNum = row["DocNum"].ToString(),
                            DocEntry = Convert.ToInt32(row["DocEntry"].ToString()),
                            DocDate = row["DocDate"].ToString(),
                            CardCode = row["CardCode"].ToString(),
                            CardName = row["CardName"].ToString(),
                            SlpCode = row["SlpCode"].ToString(),
                            SlpName = row["SlpName"].ToString(),
                            FromWhs = row["FromWhs"].ToString(),
                            ToWhs = row["ToWhs"].ToString(),
                            SeriesName = row["SeriesName"].ToString()
                        });

                    return Task.FromResult(new ResponseGetOWTQ
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        Data = oWTQ.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetOWTQ
                {
                    ErrorCode = login.lErrCode,
                    ErrorMsg = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetOWTQ
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetWTQLine> responseGetWTQLine(int DocEntry)
        {
            var lWTQ = new List<WTQLine>();
            var dtLine = new DataTable();

            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('WTQ1','{DocEntry}','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dtLine);
                    foreach (DataRow lrow in dtLine.Rows)
                        lWTQ.Add(new WTQLine
                        {
                            DocEntry = Convert.ToInt32(lrow["DocEntry"].ToString()),
                            BaseLine = Convert.ToInt32(lrow["BaseLine"].ToString()),
                            ItemCode = lrow["ItemCode"].ToString(),
                            Dscription = lrow["Dscription"].ToString(),
                            Patient = lrow["Patient"].ToString(),
                            Quantity = Convert.ToDouble(lrow["Quantity"].ToString()),
                            InputQuantity = Convert.ToDouble(lrow["InputQuantity"].ToString()),
                            FromWhsCod = lrow["FromWhsCod"].ToString(),
                            WhsCode = lrow["WhsCode"].ToString(),
                            UomCode = lrow["UomCode"].ToString(),
                            unitMsr = lrow["unitMsr"].ToString(),
                            U_unitprice = Convert.ToDouble(lrow["U_unitprice"].ToString()),
                            BinCode = lrow["BinCode"].ToString(),
                            FromBinEntry = Convert.ToInt32(lrow["FromBinEntry"].ToString()),
                            BatchYN = lrow["BatchYN"].ToString(),
                            SerialYN = lrow["SerialYN"].ToString()
                        });

                    return Task.FromResult(new ResponseGetWTQLine
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        Data = lWTQ.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetWTQLine
                {
                    ErrorCode = login.lErrCode,
                    ErrorMsg = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetWTQLine
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message,
                    Data = null
                });
            }
        }

    }
}
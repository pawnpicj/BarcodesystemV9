using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeAPIService.Models;
using BarCodeLibrary.Contract.RouteProcedure;
using BarCodeLibrary.Request.SAP.Vichika;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Vichika;
using SAPbobsCOM;

namespace BarCodeAPIService.Service.Vichika
{
    public class ReturnService:IReturnService
    {
        private int ErrCode;
        private string ErrMsg;
        #region GET
        public Task<ResponseODLNGetDelivery> responseODLNGetDelivery(string cardCode)
        {
            var getDelivery = new List<GetDelivery>();
            var getDeliveryLine = new List<GetDeliveryLine>();
            var dt = new DataTable();
            var dtLine = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var query = $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetGoodRecieptPO}','{((cardCode == null) ? "" : cardCode)}','','','','')";
                    login.AD = new OdbcDataAdapter(query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        dtLine = new DataTable();
                        var query1 = "CALL \"" + ConnectionString.CompanyDB +
                                     "\"._USP_CALLTRANS_TENGKIMLEANG('PDN1','" + row["DocEntry"] + "','','','','')";
                        login.AD = new OdbcDataAdapter(query1, login.CN);
                        login.AD.Fill(dtLine);
                        getDeliveryLine = new List<GetDeliveryLine>();
                        foreach (DataRow rowLine in dtLine.Rows)
                            getDeliveryLine.Add(new GetDeliveryLine
                            {
                                Description = rowLine["Dscription"].ToString(),
                                DiscPrcnt = Convert.ToDouble(rowLine["DiscPrcnt"].ToString()),
                                ItemCode = rowLine["ItemCode"].ToString(),
                                LineTotal = Convert.ToDouble(rowLine["LineTotal"].ToString()),
                                Price = Convert.ToDouble(rowLine["Price"].ToString()),
                                Quatity = Convert.ToDouble(rowLine["Quantity"].ToString()),
                                VatGroup = rowLine["VatGroup"].ToString(),
                                WhsCode = rowLine["WhsCode"].ToString(),
                                LineNum = Convert.ToInt32(rowLine["LineNum"].ToString()),
                                BaseEntry = Convert.ToInt32(row["DocEntry"].ToString()),
                                ManageItem = rowLine["ManageItem"].ToString(),
                            });
                        getDelivery.Add(new GetDelivery
                        {
                            CardCode = row["CardCode"].ToString(),
                            CardName = row["CardName"].ToString(),
                            CntctCode = Convert.ToInt32(row["CntctCode"].ToString()),
                            DiscPrcnt = Convert.ToDouble(row["DiscPrcnt"].ToString()),
                            DocDate = Convert.ToDateTime(row["DocDate"].ToString()),
                            DocDueDate = Convert.ToDateTime(row["DocDueDate"].ToString()),
                            DocNum = Convert.ToInt32(row["DocNum"].ToString()),
                            SlpCode = row["SlpCode"].ToString(),
                            BPCurrency = row["BPCurrency"].ToString(),
                            //DocStatus = row["DocStatus"].ToString(),
                            //DocTotal = Convert.ToDouble(row["DocTotal"].ToString()),
                            NumAtCard = row["NumAtCard"].ToString(),
                            Remark = row["Remark"].ToString(),
                            DocEntry = Convert.ToInt32(row["DocEntry"].ToString()),
                            //TaxDate = Convert.ToDateTime("2022-01-01")
                            Line = getDeliveryLine.ToList(),
                        });
                    }

                    ErrCode = login.lErrCode;
                    ErrMsg = login.sErrMsg;
                }
                else
                {
                    ErrCode = login.lErrCode;
                    ErrMsg = login.sErrMsg;
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseODLNGetDelivery
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Task.FromResult(new ResponseODLNGetDelivery
            {
                Data = getDelivery,
                ErrorCode = 0,
                ErrorMessage = ""
            });
        }

        public Task<ResponseODLNGetDelivery> responseODLNGetDeliveryByDocNum(string DocNum)
        {
            var getDelivery = new List<GetDelivery>();
            var getDeliveryLine = new List<GetDeliveryLine>();
            var dt = new DataTable();
            var dtLine = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var query = $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetGoodRecieptPO}','','{DocNum}','','','')";
                    login.AD = new OdbcDataAdapter(query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        dtLine = new DataTable();
                        var query1 = "CALL \"" + ConnectionString.CompanyDB +
                                     "\"._USP_CALLTRANS_TENGKIMLEANG('PDN1','" + row["DocEntry"] + "','','','','')";
                        login.AD = new OdbcDataAdapter(query1, login.CN);
                        login.AD.Fill(dtLine);
                        getDeliveryLine = new List<GetDeliveryLine>();
                        foreach (DataRow rowLine in dtLine.Rows)
                            getDeliveryLine.Add(new GetDeliveryLine
                            {
                                Description = rowLine["Dscription"].ToString(),
                                DiscPrcnt = Convert.ToDouble(rowLine["DiscPrcnt"].ToString()),
                                ItemCode = rowLine["ItemCode"].ToString(),
                                LineTotal = Convert.ToDouble(rowLine["LineTotal"].ToString()),
                                Price = Convert.ToDouble(rowLine["Price"].ToString()),
                                Quatity = Convert.ToDouble(rowLine["Quantity"].ToString()),
                                VatGroup = rowLine["VatGroup"].ToString(),
                                WhsCode = rowLine["WhsCode"].ToString(),
                                LineNum = Convert.ToInt32(rowLine["LineNum"].ToString()),
                                BaseEntry = Convert.ToInt32(row["DocEntry"].ToString()),
                                ManageItem = rowLine["ManageItem"].ToString(),
                            });
                        getDelivery.Add(new GetDelivery
                        {
                            CardCode = row["CardCode"].ToString(),
                            CardName = row["CardName"].ToString(),
                            CntctCode = Convert.ToInt32(row["CntctCode"].ToString()),
                            DiscPrcnt = Convert.ToDouble(row["DiscPrcnt"].ToString()),
                            DocDate = Convert.ToDateTime(row["DocDate"].ToString()),
                            DocDueDate = Convert.ToDateTime(row["DocDueDate"].ToString()),
                            DocNum = Convert.ToInt32(row["DocNum"].ToString()),
                            SlpCode = row["SlpCode"].ToString(),
                            BPCurrency = row["BPCurrency"].ToString(),
                            SlpName = row["SlpName"].ToString(),
                            //DocStatus = row["DocStatus"].ToString(),
                            //DocTotal = Convert.ToDouble(row["DocTotal"].ToString()),
                            NumAtCard = row["NumAtCard"].ToString(),
                            Remark = row["Remark"].ToString(),
                            DocEntry = Convert.ToInt32(row["DocEntry"].ToString()),
                            //TaxDate = Convert.ToDateTime("2022-01-01")
                            Line = getDeliveryLine.ToList(),
                        });
                    }

                    ErrCode = login.lErrCode;
                    ErrMsg = login.sErrMsg;
                }
                else
                {
                    ErrCode = login.lErrCode;
                    ErrMsg = login.sErrMsg;
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseODLNGetDelivery
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Task.FromResult(new ResponseODLNGetDelivery
            {
                Data = getDelivery,
                ErrorCode = 0,
                ErrorMessage = ""
            });
        }
        #endregion
        #region POST
        public Task<ResponseReturn> sendGoodReturn(SendReturn sendGoodReturn)
        {
            try
            {
                Documents oGoodReturnPurchase;
                Company oCompany;
                var Retval = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    oGoodReturnPurchase = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oReturns);
                    oGoodReturnPurchase.CardCode = sendGoodReturn.CardCode;
                    oGoodReturnPurchase.DocDate = sendGoodReturn.DocDate;
                    oGoodReturnPurchase.DocDueDate = sendGoodReturn.DocDueDate;
                    oGoodReturnPurchase.NumAtCard = sendGoodReturn.NumAtCard;
                    oGoodReturnPurchase.Comments = sendGoodReturn.Remark;
                    oGoodReturnPurchase.Series = sendGoodReturn.Series;
                    oGoodReturnPurchase.DocCurrency = sendGoodReturn.BPCurrency;
                    foreach (var l in sendGoodReturn.Lines)
                    {
                        oGoodReturnPurchase.Lines.ItemCode = l.ItemCode;
                        oGoodReturnPurchase.Lines.Quantity = l.Quantity;
                        oGoodReturnPurchase.Lines.UnitPrice = l.UnitPrice;
                        oGoodReturnPurchase.Lines.WarehouseCode = l.WarehouseCode;
                        oGoodReturnPurchase.Lines.TaxCode = l.TaxCode;
                        oGoodReturnPurchase.Lines.BaseEntry = l.BaseEntry;
                        oGoodReturnPurchase.Lines.BaseLine = l.LineNum;
                        oGoodReturnPurchase.Lines.BaseType = 20;
                        if (l.ManageItem == "S")
                        {
                            foreach (DataRow rowSerial in GetBatchSerialNumber("GetSerialNumber", l.BaseEntry.ToString(), l.LineNum.ToString(), l.ItemCode).Rows)
                            {
                                oGoodReturnPurchase.Lines.SerialNumbers.Quantity = Convert.ToDouble(rowSerial["Quantity"].ToString());
                                oGoodReturnPurchase.Lines.SerialNumbers.InternalSerialNumber = rowSerial["DistNumber"].ToString();
                                oGoodReturnPurchase.Lines.SerialNumbers.Add();
                            }
                        }
                        else if (l.ManageItem == "B")
                            foreach (DataRow rowBatch in GetBatchSerialNumber("GetBatchNumber", l.BaseEntry.ToString(), l.LineNum.ToString(), l.ItemCode).Rows)
                            {
                                oGoodReturnPurchase.Lines.BatchNumbers.Quantity = Convert.ToDouble(rowBatch["Quantity"].ToString());
                                oGoodReturnPurchase.Lines.BatchNumbers.BatchNumber = rowBatch["BatchNum"].ToString();
                                oGoodReturnPurchase.Lines.BatchNumbers.Add();
                            }
                        oGoodReturnPurchase.Lines.Add();
                    }
                    Retval = oGoodReturnPurchase.Add();
                    if (Retval != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);
                        return Task.FromResult(new ResponseReturn
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = null
                        });
                    }

                    return Task.FromResult(new ResponseReturn
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        DocEntry = oCompany.GetNewObjectKey()
                    });
                }

                return Task.FromResult(new ResponseReturn
                {
                    ErrorCode = login.LErrCode,
                    ErrorMsg = login.SErrMsg
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseReturn
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        #endregion
        #region Other Function
        DataTable GetBatchSerialNumber(string type, string BaseEntry, string LineNum, string ItemCode)
        {
            var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
            if (login.lErrCode == 0)
            {
                DataTable dtBatchSerial = new DataTable();
                var query1 = "CALL \"" + ConnectionString.CompanyDB +
                             "\"._USP_CALLTRANS_TENGKIMLEANG('" + type + "','" + BaseEntry + "','" + LineNum + "','" + ItemCode + "','','')";
                login.AD = new OdbcDataAdapter(query1, login.CN);
                login.AD.Fill(dtBatchSerial);
                return dtBatchSerial;
            }

            return null;


        }
        #endregion
    }
}

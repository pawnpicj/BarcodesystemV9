using BarCodeAPIService.Connection;
using BarCodeAPIService.Models;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.Contract.RouteProcedure;
using BarCodeLibrary.Respones.SAP.Tengkimleang;
using BarCodeLibrary.Respones.SAP.Vichika;
using SAPbobsCOM;

namespace BarCodeAPIService.Service
{
    public class DeliveryService : IDeliveryService
    {
        private int ErrCode;
        private string ErrMsg;
        public Task<ResponseDelivery> PostDelivery(SendDelivery sendDelivery)
        {
            try
            {
                Documents oDeliveryDocuments;
                Company oCompany;
                var Retval = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    oDeliveryDocuments = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    oDeliveryDocuments.CardCode = sendDelivery.CardCode;
                    oDeliveryDocuments.DocDate = sendDelivery.DocDate;
                    oDeliveryDocuments.DocDueDate = sendDelivery.DocDate;
                    oDeliveryDocuments.Series = Convert.ToInt32(sendDelivery.Series);
                    oDeliveryDocuments.DocCurrency = sendDelivery.CurrencyCode;
                    oDeliveryDocuments.Comments = (sendDelivery.Remark == null) ? "" : sendDelivery.Remark;
                    oDeliveryDocuments.SalesPersonCode = Convert.ToInt32(sendDelivery.SlpCode);
                    oDeliveryDocuments.UserFields.Fields.Item("U_WebID").Value = sendDelivery.Series + DateTime.Today.Day
                        + DateTime.Today.Month
                        + DateTime.Today.Year
                        + DateTime.Today.DayOfYear
                        + DateTime.Today.Hour
                        + DateTime.Today.Minute
                        + DateTime.Today.Second
                        + DateTime.Today.Millisecond;
                    foreach (var l in sendDelivery.Lines)
                    {
                        oDeliveryDocuments.Lines.ItemCode = l.ItemCode;
                        oDeliveryDocuments.Lines.Quantity = l.Quantity;
                        oDeliveryDocuments.Lines.UnitPrice = l.PriceBeforeDis;
                        oDeliveryDocuments.Lines.DiscountPercent = l.Discount;
                        oDeliveryDocuments.Lines.WarehouseCode = l.Whs;
                       // oDeliveryDocuments.Lines.UoMEntry = Convert.ToInt32(l.UomName);
                        if (l.DocEntry != null)
                        {
                            oDeliveryDocuments.Lines.BaseEntry = Convert.ToInt32(l.DocEntry);
                            oDeliveryDocuments.Lines.BaseType = 17;
                            oDeliveryDocuments.Lines.BaseLine = l.LineNum;
                        }

                        if (l.ManageItem == "S")
                            foreach (var serial in l.Serial)
                            {
                                oDeliveryDocuments.Lines.SerialNumbers.Quantity = 1;
                                //oGoodReceiptPO.Lines.SerialNumbers.ManufacturerSerialNumber = serial.MfrSerialNo;
                                oDeliveryDocuments.Lines.SerialNumbers.InternalSerialNumber = serial.SerialNumber;
                                oDeliveryDocuments.Lines.SerialNumbers.Add();
                            }
                        else if (l.ManageItem == "B")
                            foreach (var batch in l.Batches)
                            {
                                //oGoodReceiptPO.Lines.BatchNumbers.AddmisionDate = batch.AdmissionDate;
                                oDeliveryDocuments.Lines.BatchNumbers.ExpiryDate = Convert.ToDateTime(batch.ExpDate);
                                //oGoodReceiptPO.Lines.BatchNumbers.ManufacturingDate = batch.MfrDate;
                                oDeliveryDocuments.Lines.BatchNumbers.Quantity = batch.InputQty;
                                oDeliveryDocuments.Lines.BatchNumbers.BatchNumber = batch.BatchNumber;
                                oDeliveryDocuments.Lines.BatchNumbers.Add();
                            }

                        oDeliveryDocuments.Lines.Add();
                    }

                    Retval = oDeliveryDocuments.Add();
                    if (Retval != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);
                        return Task.FromResult(new ResponseDelivery
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = null
                        });
                    }

                    return Task.FromResult(new ResponseDelivery
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        DocEntry = oCompany.GetNewObjectKey()
                    });
                }

                return Task.FromResult(new ResponseDelivery
                {
                    ErrorCode = login.LErrCode,
                    ErrorMsg = login.SErrMsg
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseDelivery
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }

        public Task<ResponseGetSaleOrder> responseGetSaleOrder()
        {
            var getSaleOrder = new List<GetSaleOrder>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetSaleOrder}','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        getSaleOrder.Add(new GetSaleOrder
                        {
                            DocNum = row["DocNum"].ToString(),
                            CardCode = row["CardCode"].ToString(),
                            CardName = row["CardName"].ToString(),
                            AddressFrom = row["AddressFrom"].ToString(),
                            AddressTo = row["AddressTo"].ToString(),
                            DeliveryDate = Convert.ToDateTime(row["DeliveryDate"]).ToShortDateString()
                        });
                    return Task.FromResult(new ResponseGetSaleOrder
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getSaleOrder.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetSaleOrder
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetSaleOrder
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetORDR> responseGetORDR(string cardName)
        {
            var oPORs = new List<ORDR>();
            var pOR1s = new List<RDR1>();
            var dt = new DataTable();
            var dtLine = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetSO}','{cardName}','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        dtLine = new DataTable();
                        Query =
                            $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetSOLine}','{row["DocENtry"]}','','','','')";
                        login.AD = new OdbcDataAdapter(Query, login.CN);
                        login.AD.Fill(dtLine);
                        pOR1s = new List<RDR1>();
                        foreach (DataRow drLine in dtLine.Rows)
                            pOR1s.Add(new RDR1
                            {
                                ItemCode = drLine["ItemCode"].ToString(),
                                Description = drLine["Description"].ToString(),
                                Quatity = Convert.ToDouble(drLine["Quantity"].ToString()),
                                Price = Convert.ToDouble(drLine["Price"].ToString()),
                                PriceBeforeDis = Convert.ToDouble(drLine["PriceBefDi"].ToString()),
                                DiscPrcnt = Convert.ToDouble(drLine["DiscPrcnt"].ToString()),
                                DiscountAMT = Convert.ToDouble(drLine["DiscountAmt"].ToString()),
                                VatGroup = drLine["LineTotal"].ToString(),
                                WhsCode = drLine["WhsCode"].ToString(),
                                LineTotal = Convert.ToDouble(drLine["LineTotal"].ToString()),
                                ManageItem = drLine["ManageItem"].ToString(),
                                UomName = drLine["UomName"].ToString(),
                                TaxCode = drLine["TaxCode"].ToString(),
                                LineNum = Convert.ToInt32(drLine["LineNum"].ToString())
                            });
                        oPORs.Add(new ORDR
                        {
                            DocEntry = Convert.ToInt32(row["DocENtry"].ToString()),
                            CardCode = row["CardCode"].ToString(),
                            CardName = row["CardName"].ToString(),
                            CntctCode = Convert.ToInt32(row["CntctCode"].ToString()),
                            NumAtCard = row["NumAtCard"].ToString(),
                            DocNum = Convert.ToInt32(row["DocNum"].ToString()),
                            DocStatus = row["DocStatus"].ToString(),
                            DocDate = Convert.ToDateTime(row["DocDate"]).ToShortDateString(),
                            DocDueDate = Convert.ToDateTime(row["DocDueDate"]).ToShortDateString(),
                            TaxDate = Convert.ToDateTime(row["TaxDate"]).ToShortDateString(),
                            DocTotal = Convert.ToDouble(row["DocTotal"]),
                            DiscPrcnt = Convert.ToDouble(row["DiscPrcnt"]),
                            DiscountAMT = Convert.ToDouble(row["DiscSum"].ToString()),
                            Line = pOR1s.ToList()
                        });
                    }

                    return Task.FromResult(new ResponseGetORDR
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oPORs.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetORDR
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetORDR
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetBatch> responseGetBatch(string ItemCode, string WhsCode)
        {
            var batchGets = new List<GetBatch>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetBatch}','{ItemCode}','{WhsCode}','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        batchGets.Add(new GetBatch
                        {
                            ItemCode = row["ItemCode"].ToString(),
                            BatchNumber = row["DistNumber"].ToString(),
                            Qty = Convert.ToDouble(row["Quantity"].ToString()),
                            ExpDate = row["ExpDate"].ToString(),
                            InputQty = Convert.ToDouble(row["InputQty"].ToString())
                        });
                    return Task.FromResult(new ResponseGetBatch
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = batchGets.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetBatch
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetBatch
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
        public Task<ResponseGetSerial> responseGetSerial(string ItemCode, string WhsCode)
        {
            var serialGets = new List<GetSerial>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetSerial}','{ItemCode}','{WhsCode}','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        serialGets.Add(new GetSerial
                        {
                            ItemCode = row["ItemCode"].ToString(),
                            SerialNumber = row["Serial"].ToString(),
                            Qty = Convert.ToDouble(row["Qty"].ToString())
                        });
                    return Task.FromResult(new ResponseGetSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = serialGets.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetSerial
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

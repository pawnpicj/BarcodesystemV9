using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeAPIService.Models;
using BarCodeLibrary.Contract.RouteProcedure;
using BarCodeLibrary.Request.SAP.TengKimleang;
using BarCodeLibrary.Respones.SAP.Tengkimleang;
using SAPbobsCOM;

namespace BarCodeAPIService.Service
{
    public class GoodsReceiptPOService : IGoodsReceiptPOService
    {
        private int ErrCode;
        private string ErrMsg;

        #region Post

        public Task<ResponseGoodReceiptPO> PostGoodReceiptPO(SendGoodReceiptPO sendGoodReceiptPO)
        {
            try
            {
                Documents oGoodReceiptPO;
                Company oCompany;
                var Retval = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    oGoodReceiptPO = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes);
                    oGoodReceiptPO.CardCode = sendGoodReceiptPO.CardCode;
                    oGoodReceiptPO.DocDate = sendGoodReceiptPO.DocDate;
                    oGoodReceiptPO.BPL_IDAssignedToInvoice = sendGoodReceiptPO.BrandID;
                    foreach (var l in sendGoodReceiptPO.Line)
                    {
                        oGoodReceiptPO.Lines.ItemCode = l.ItemCode;
                        oGoodReceiptPO.Lines.Quantity = l.Qty;
                        oGoodReceiptPO.Lines.UnitPrice = l.UnitPrice;
                        oGoodReceiptPO.Lines.WarehouseCode = l.WhsCode;
                        oGoodReceiptPO.Lines.UoMEntry = l.UomCode;
                        oGoodReceiptPO.Lines.Add();
                    }

                    Retval = oGoodReceiptPO.Add();
                    if (Retval != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);
                        return Task.FromResult(new ResponseGoodReceiptPO
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = null
                        });
                    }

                    return Task.FromResult(new ResponseGoodReceiptPO
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        DocEntry = oCompany.GetNewObjectKey()
                    });
                }

                return Task.FromResult(new ResponseGoodReceiptPO
                {
                    ErrorCode = login.LErrCode,
                    ErrorMsg = login.SErrMsg
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGoodReceiptPO
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }

        #endregion

        #region Get

        public Task<ResponseCustomerGet> responseCustomerGets()
        {
            var customerGets = new List<CustomerGet>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.CustomerGet}','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        customerGets.Add(new CustomerGet
                        {
                            CardCode = row["CardCode"].ToString(),
                            CardName = row["CardName"].ToString(),
                            Address = row["Address"].ToString(),
                            Phone = row["Phone"].ToString()
                        });
                    return Task.FromResult(new ResponseCustomerGet
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = customerGets.ToList()
                    });
                }

                return Task.FromResult(new ResponseCustomerGet
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseCustomerGet
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseOPORGetPO> responseOPORGetPO(string cardName)
        {
            var oPORs = new List<OPOR>();
            var pOR1s = new List<POR1>();
            var dt = new DataTable();
            var dtLine = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetPO}','{cardName}','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        dtLine = new DataTable();
                        Query =
                            $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetPOLine}','{row["DocENtry"]}','','','','')";
                        login.AD = new OdbcDataAdapter(Query, login.CN);
                        login.AD.Fill(dtLine);
                        pOR1s = new List<POR1>();
                        foreach (DataRow drLine in dtLine.Rows)
                            pOR1s.Add(new POR1
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
                        oPORs.Add(new OPOR
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

                    return Task.FromResult(new ResponseOPORGetPO
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oPORs.ToList()
                    });
                }

                return Task.FromResult(new ResponseOPORGetPO
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOPORGetPO
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetSeries> responseGetSeries(string objectCode, string dateOfMonth)
        {
            var getSeries = new List<GetSeries>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetSeries}','{objectCode}','{dateOfMonth}','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        getSeries.Add(new GetSeries
                        {
                            Code = Convert.ToInt32(row["Code"].ToString()),
                            Name = row["Name"].ToString()
                        });
                    return Task.FromResult(new ResponseGetSeries
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getSeries.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetSeries
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetSeries
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetSaleEmployee> responseGetSaleEmployees()
        {
            var getSaleEmployees = new List<GetSaleEmployee>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetSaleEmployee}','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        getSaleEmployees.Add(new GetSaleEmployee
                        {
                            Code = Convert.ToInt32(row["Code"].ToString()),
                            Name = row["Name"].ToString()
                        });
                    return Task.FromResult(new ResponseGetSaleEmployee
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getSaleEmployees.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetSaleEmployee
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetSaleEmployee
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetCurrency> responseGetCurrency(string cardCode)
        {
            var getCurrencies = new List<GetCurrency>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    var Query =
                        $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetCurrency}','{cardCode}','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        getCurrencies.Add(new GetCurrency
                        {
                            Code = row["Code"].ToString(),
                            Name = row["Name"].ToString()
                        });
                    return Task.FromResult(new ResponseGetCurrency
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getCurrencies.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetCurrency
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetCurrency
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetGenerateBatchSerial> responseGetGenerateBatchSerial()
        {
            return null;
            //var getCurrencies = new List<GetCurrency>();
            //var dt = new DataTable();
            //try
            //{
            //    var login = new LoginOnlyDatabase();
            //    if (login.lErrCode == 0)
            //    {
            //        var Query =
            //            $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetCurrency}','{cardCode}','','','','')";
            //        login.AD = new OdbcDataAdapter(Query, login.CN);
            //        login.AD.Fill(dt);
            //        foreach (DataRow row in dt.Rows)
            //            getCurrencies.Add(new GetCurrency
            //            {
            //                Code = row["Code"].ToString(),
            //                Name = row["Name"].ToString()
            //            });
            //        return Task.FromResult(new ResponseGetCurrency
            //        {
            //            ErrorCode = 0,
            //            ErrorMessage = "",
            //            Data = getCurrencies.ToList()
            //        });
            //    }

            //    return Task.FromResult(new ResponseGetCurrency
            //    {
            //        ErrorCode = login.lErrCode,
            //        ErrorMessage = login.sErrMsg,
            //        Data = null
            //    });
            //}

            //catch (Exception ex)
            //{
            //    return Task.FromResult(new ResponseGetCurrency
            //    {
            //        ErrorCode = ex.HResult,
            //        ErrorMessage = ex.Message,
            //        Data = null
            //    });
            //}
        }

        #endregion
    }
}
using BarCodeAPIService.Connection;
using BarCodeAPIService.Models;
using BarCodeLibrary.Contract.RouteProcedure;
using BarCodeLibrary.Request.SAP.TengKimleang;
using BarCodeLibrary.Respones.SAP.Tengkimleang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
                SAPbobsCOM.Documents oGoodReceiptPO;
                SAPbobsCOM.Company oCompany;
                int Retval = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    oGoodReceiptPO = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes);
                    oGoodReceiptPO.CardCode = sendGoodReceiptPO.CardCode;
                    oGoodReceiptPO.DocDate = sendGoodReceiptPO.DocDate;
                    oGoodReceiptPO.BPL_IDAssignedToInvoice = sendGoodReceiptPO.BrandID;
                    foreach (SendGoodReceiptPOLine l in sendGoodReceiptPO.Line)
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
                    else
                    {
                        return Task.FromResult(new ResponseGoodReceiptPO
                        {
                            ErrorCode = 0,
                            ErrorMsg = "",
                            DocEntry = oCompany.GetNewObjectKey(),
                        });
                    }

                }
                else
                {
                    return Task.FromResult(new ResponseGoodReceiptPO
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg
                    });
                }
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
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.CustomerGet}','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        customerGets.Add(new CustomerGet
                        {
                            CardCode = row["CardCode"].ToString(),
                            CardName = row["CardName"].ToString(),
                            Address = row["Address"].ToString(),
                            Phone = row["Phone"].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseCustomerGet
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = customerGets.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseCustomerGet
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
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
            DataTable dt = new DataTable();
            DataTable dtLine = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetPO}','{cardName}','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        dtLine = new DataTable();
                        Query = $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetPOLine}','{row["DocENtry"]}','','','','')";
                        login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                        login.AD.Fill(dtLine);
                        pOR1s = new List<POR1>();
                        foreach (DataRow drLine in dtLine.Rows)
                        {
                            pOR1s.Add(new POR1
                            {
                                ItemCode = drLine["ItemCode"].ToString(),
                                Description = drLine["Description"].ToString(),
                                Quatity = Convert.ToDouble(drLine["Quantity"].ToString()),
                                Price = Convert.ToDouble(drLine["Price"].ToString()),
                                DiscPrcnt = Convert.ToDouble(drLine["DiscPrcnt"].ToString()),
                                VatGroup = drLine["LineTotal"].ToString(),
                                WhsCode = drLine["WhsCode"].ToString(),
                                LineTotal = Convert.ToDouble(drLine["LineTotal"].ToString()),
                                ManageItem = drLine["ManageItem"].ToString()
                            });
                        }
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
                else
                {
                    return Task.FromResult(new ResponseOPORGetPO
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
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
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetSeries}','{objectCode}','{dateOfMonth}','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        getSeries.Add(new GetSeries
                        {
                            Code = Convert.ToInt32(row["Code"].ToString()),
                            Name=row["Name"].ToString(),
                        });
                    }
                    return Task.FromResult(new ResponseGetSeries
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getSeries.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseGetSeries
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
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
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = $"CALL \"{ConnectionString.CompanyDB}\".{ProcedureRoute._USP_CALLTRANS_TENGKIMLEANG} ('{ProcedureRoute.Type.GetSaleEmployee}','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        getSaleEmployees.Add(new GetSaleEmployee
                        {
                            Code = Convert.ToInt32(row["Code"].ToString()),
                            Name = row["Name"].ToString(),
                        });
                    }
                    return Task.FromResult(new ResponseGetSaleEmployee
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = getSaleEmployees.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseGetSaleEmployee
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
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
        #endregion
    }
}

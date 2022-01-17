using BarCodeAPIService.Connection;
using BarCodeAPIService.Models;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class GoodReturnService : IGoodReturnService
    {
        private int ErrCode;
        private string ErrMsg;

        public Task<ResponseOPDNGetGoodReceipt> responseOPDNGetGoodReceipt()
        {
            var oPDNs = new List<OPDN>();
            var pDN1s = new List<PDN1>();
            DataTable dt = new DataTable();
            DataTable dtLine = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_TENGKIMLEANG('OPDN','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        dtLine = new DataTable();
                        string query1 = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_TENGKIMLEANG('PDN1','"+row["DocEntry"].ToString()+"','','','','')";
                        login.AD = new System.Data.Odbc.OdbcDataAdapter(query1, login.CN);
                        login.AD.Fill(dtLine);
                        pDN1s = new List<PDN1>();
                        foreach (DataRow rowLine in dtLine.Rows)
                        {
                            pDN1s.Add(new PDN1  
                            {
                                Description= rowLine["Dscription"].ToString(),
                                DiscPrcnt=Convert.ToDouble(rowLine["DiscPrcnt"].ToString()),
                                ItemCode=rowLine["ItemCode"].ToString(),
                                LineTotal=Convert.ToDouble(rowLine["LineTotal"].ToString()),
                                Price=Convert.ToDouble(rowLine["Price"].ToString()),
                                Quatity=Convert.ToDouble(rowLine["Quantity"].ToString()),
                                VatGroup= rowLine["VatGroup"].ToString(),
                                WhsCode= rowLine["WhsCode"].ToString()
                            });
                        }
                        oPDNs.Add(new OPDN
                        {
                            CardCode = row["CardCode"].ToString(),
                            CardName = row["CardName"].ToString(),
                            CntctCode = Convert.ToInt32(row["CntctCode"].ToString()),
                            DiscPrcnt = Convert.ToDouble(row["DiscPrcnt"].ToString()),
                            DocDate = Convert.ToDateTime(row["DocDate"].ToString()),
                            DocDueDate = Convert.ToDateTime(row["DocDueDate"].ToString()),
                            DocNum = Convert.ToInt32(row["DocNum"].ToString()),
                            //DocStatus = row["DocStatus"].ToString(),
                            //DocTotal = Convert.ToDouble(row["DocTotal"].ToString()),
                            Line = pDN1s.ToList(),
                            //NumAtCard = row["NumAtCard"].ToString(),
                            //TaxDate = Convert.ToDateTime("2022-01-01")
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
            catch(Exception ex)
            {
                return Task.FromResult(new ResponseOPDNGetGoodReceipt
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return Task.FromResult(new ResponseOPDNGetGoodReceipt
            {
                Data=oPDNs,
                ErrorCode=0,
                ErrorMessage=""
            });
        }

        public Task<ResponseGoodReturn> sendGoodReturn(SendGoodsReturn sendGoodReturn)
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
                    oGoodReceiptPO = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseReturns);
                    oGoodReceiptPO.CardCode = sendGoodReturn.CardCode;
                    oGoodReceiptPO.DocDate = sendGoodReturn.DocDate;
                    foreach (SendGoodsReturnLine l in sendGoodReturn.Lines)
                    {
                        oGoodReceiptPO.Lines.ItemCode = l.ItemCode;
                        oGoodReceiptPO.Lines.Quantity = l.Quantity;
                        oGoodReceiptPO.Lines.UnitPrice = l.UnitPrice;
                        oGoodReceiptPO.Lines.WarehouseCode = l.WarehouseCode;
                        oGoodReceiptPO.Lines.UoMEntry = l.UomCode;
                        oGoodReceiptPO.Lines.Add();
                    }
                    Retval = oGoodReceiptPO.Add();
                    if (Retval != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);
                        return Task.FromResult(new ResponseGoodReturn
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = null
                        });
                    }
                    else
                    {
                        return Task.FromResult(new ResponseGoodReturn
                        {
                            ErrorCode = 0,
                            ErrorMsg = "",
                            DocEntry = oCompany.GetNewObjectKey(),
                        });
                    }

                }
                else
                {
                    return Task.FromResult(new ResponseGoodReturn
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGoodReturn
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
    }
}

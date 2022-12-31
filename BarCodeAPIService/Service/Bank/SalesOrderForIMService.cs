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
    public class SalesOrderForIMService : ISalesOrderForIMService
    {
        private int ErrCode;
        private string ErrMsg;

        public Task<ResponseSalesOrder> PostSalesOrder(SendSalesOrderForIM sendSalesOrderForIM)
        {
            try
            {
                Documents oSalesOrderDocuments;
                Company oCompany;
                var Retval = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    oSalesOrderDocuments = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oOrders);
                    oSalesOrderDocuments.CardCode = sendSalesOrderForIM.CardCode;
                    oSalesOrderDocuments.DocDate = sendSalesOrderForIM.DocDate;
                    oSalesOrderDocuments.DocDueDate = sendSalesOrderForIM.DocDueDate;
                    oSalesOrderDocuments.TaxDate = sendSalesOrderForIM.TaxDate;
                    oSalesOrderDocuments.Series = Convert.ToInt32(sendSalesOrderForIM.SeriesCode);
                    //oSalesOrderDocuments.DocCurrency = sendSalesOrderForIM.CurrencyCode;
                    oSalesOrderDocuments.Comments = (sendSalesOrderForIM.Remark == null) ? "" : sendSalesOrderForIM.Remark;
                    oSalesOrderDocuments.SalesPersonCode = Convert.ToInt32(sendSalesOrderForIM.SlpCode);
                    oSalesOrderDocuments.UserFields.Fields.Item("U_WebID").Value = sendSalesOrderForIM.SeriesCode + DateTime.Today.Day
                        + DateTime.Today.Month
                        + DateTime.Today.Year
                        + DateTime.Today.DayOfYear
                        + DateTime.Today.Hour
                        + DateTime.Today.Minute
                        + DateTime.Today.Second;
                    foreach (var l in sendSalesOrderForIM.Lines)
                    {
                        oSalesOrderDocuments.Lines.ItemCode = l.ItemCode;
                        oSalesOrderDocuments.Lines.Quantity = l.Quantity;
                        oSalesOrderDocuments.Lines.Price = l.Price;
                        oSalesOrderDocuments.Lines.WarehouseCode = l.WhsCode;
                        if (l.U_TranferNo != "" && l.U_TranferNo != "-")
                        {
                            oSalesOrderDocuments.Lines.UserFields.Fields.Item("U_TranferNo").Value = l.U_TranferNo;
                        }

                        if (l.U_Patient != "")
                        {
                            oSalesOrderDocuments.Lines.UserFields.Fields.Item("U_Patient").Value = l.U_Patient;
                        }
                        

                        if (l.ManageItem == "S")
                            foreach (var serial in l.Serial)
                            {
                                oSalesOrderDocuments.Lines.SerialNumbers.Quantity = 1;
                                oSalesOrderDocuments.Lines.SerialNumbers.InternalSerialNumber = serial.SerialNumber;
                                oSalesOrderDocuments.Lines.SerialNumbers.ManufacturerSerialNumber = serial.SerialNumber;
                                oSalesOrderDocuments.Lines.SerialNumbers.Add();
                            }
                        else if (l.ManageItem == "B")
                            foreach (var batch in l.Batches)
                            {
                                oSalesOrderDocuments.Lines.BatchNumbers.ExpiryDate = Convert.ToDateTime(batch.ExpDate);
                                oSalesOrderDocuments.Lines.BatchNumbers.Quantity = batch.Quantity;
                                oSalesOrderDocuments.Lines.BatchNumbers.BatchNumber = batch.BatchNumber;
                                oSalesOrderDocuments.Lines.BatchNumbers.Add();
                            }

                        oSalesOrderDocuments.Lines.Add();
                    }

                    Retval = oSalesOrderDocuments.Add();
                    if (Retval != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);
                        return Task.FromResult(new ResponseSalesOrder
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = null
                        });
                    }
                    else
                    {                        
                        return Task.FromResult(new ResponseSalesOrder
                        {
                            ErrorCode = 0,
                            ErrorMsg = "",
                            DocEntry = oCompany.GetNewObjectKey()
                        });
                    }
                    
                }

                return Task.FromResult(new ResponseSalesOrder
                {
                    ErrorCode = login.LErrCode,
                    ErrorMsg = login.SErrMsg
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseSalesOrder
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }

        }
    }
}

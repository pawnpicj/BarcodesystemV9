using BarCodeAPIService.Connection;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BarCodeAPIService.Models;
using System.Data;
using System.Data.Odbc;
using SAPbobsCOM;

namespace BarCodeAPIService.Service
{
    public class InventoryTransferService : IInventoryTransferService
    {
        private int ErrCode;
        private string ErrMsg;
        public double BQty;
        public Task<ResponseInventoryTransfer> responseInventoryTransfer(SendInventoryTransfer sendInventoryTransfer)
        {
            try
            {
                SAPbobsCOM.StockTransfer oStockTransfer;
                SAPbobsCOM.Company oCompany;
                int Retval = 0;
                int i = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    string strWebID = "";
                    string strToBinCode = "";
                    string txt_U_pname = "";
                    var date = DateTime.Now;
                    strWebID = sendInventoryTransfer.Series + "-" + date.Day + "" + date.Month + "" + date.Year + "-" + date.Hour + "" + date.Minute + "" + date.Second;
                    double sumQty = 0;
                    double BalanceQty = 0;
                    int n;
                    oCompany = login.Company;

                    oStockTransfer = (StockTransfer)oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);

                    //==== Head ====
                    //Head                    
                    oStockTransfer.Series = sendInventoryTransfer.Series;
                    oStockTransfer.CardCode = sendInventoryTransfer.CardCode;
                    oStockTransfer.DocDate = sendInventoryTransfer.DocDate;
                    oStockTransfer.FromWarehouse = sendInventoryTransfer.FromWhsCode;
                    oStockTransfer.ToWarehouse = sendInventoryTransfer.ToWhsCode;
                    oStockTransfer.UserFields.Fields.Item("U_WebID").Value = strWebID;
                    oStockTransfer.UserFields.Fields.Item("U_ToBinLoc").Value = "" + sendInventoryTransfer.ToBinCode;                    
                    oStockTransfer.UserFields.Fields.Item("U_loannum").Value = sendInventoryTransfer.U_loannum;

                    strToBinCode = sendInventoryTransfer.ToBinCode;
                    if (sendInventoryTransfer.U_pname == null || sendInventoryTransfer.U_pname == "")
                    {
                        txt_U_pname = " ";
                    }
                    else
                    {
                        txt_U_pname = sendInventoryTransfer.U_pname;
                    }
                    oStockTransfer.UserFields.Fields.Item("U_pname").Value = txt_U_pname;
                    //==== Line ====
                    int lineNumb = 0;
                    foreach (SendInventoryTransferLine l in sendInventoryTransfer.Line)
                    {
                        sumQty = 0;
                        BalanceQty = 0;

                        if (l.BatchLine != null || l.SerialLine != null)
                        {

                            oStockTransfer.Lines.SetCurrentLine(lineNumb);

                            oStockTransfer.Lines.BaseEntry = l.BaseEntry;
                            oStockTransfer.Lines.BaseLine = l.BaseLine;
                            oStockTransfer.Lines.BaseType = SAPbobsCOM.InvBaseDocTypeEnum.InventoryTransferRequest;

                            oStockTransfer.Lines.ItemCode = l.ItemCode;
                            oStockTransfer.Lines.FromWarehouseCode = l.FromWhsCode;
                            oStockTransfer.Lines.WarehouseCode = l.ToWhsCode;
                            oStockTransfer.Lines.Quantity = l.QtyInSap;

                            string strPatient = "";
                            string xPatient = "";
                            strPatient = l.U_Patient;
                            if (strPatient is not null)
                            {
                                xPatient = strPatient;
                            }
                            else
                            {
                                xPatient = " ";
                            }
                            oStockTransfer.Lines.UserFields.Fields.Item("U_Patient").Value = xPatient;

                            //if (l.U_TranferNo != "" && l.U_TranferNo != "-")
                            //{
                            //    oStockTransfer.Lines.UserFields.Fields.Item("U_TranferNo").Value = l.U_TranferNo;
                            //}

                            if (l.ProductType == "b" && l.BatchLine != null)
                            {
                                int nB = 0;
                                foreach (var batch in l.BatchLine)
                                {
                                    oStockTransfer.Lines.BatchNumbers.SetCurrentLine(nB);
                                    oStockTransfer.Lines.BatchNumbers.BatchNumber = batch.BatchNumber;
                                    oStockTransfer.Lines.BatchNumbers.Quantity = batch.Quantity;
                                    oStockTransfer.Lines.BatchNumbers.Add();

                                    oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse;
                                    oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = nB;
                                    oStockTransfer.Lines.BinAllocations.BinAbsEntry = batch.fromBinEntryX;
                                    oStockTransfer.Lines.BinAllocations.Quantity = batch.Quantity;
                                    oStockTransfer.Lines.BinAllocations.Add();

                                    oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse;
                                    oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = nB;
                                    oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.toBinEntry;
                                    oStockTransfer.Lines.BinAllocations.Quantity = batch.Quantity;
                                    oStockTransfer.Lines.BinAllocations.Add();

                                    sumQty = sumQty + batch.Quantity;
                                    nB++;
                                }
                            }
                            else if (l.ProductType == "s" && l.SerialLine != null)
                            {
                                int nS = 0;
                                foreach (var serial in l.SerialLine)
                                {
                                    oStockTransfer.Lines.SerialNumbers.SetCurrentLine(nS);
                                    oStockTransfer.Lines.SerialNumbers.ManufacturerSerialNumber = serial.SerialNumber;
                                    oStockTransfer.Lines.SerialNumbers.InternalSerialNumber = serial.SerialNumber;
                                    oStockTransfer.Lines.SerialNumbers.Quantity = 1;
                                    oStockTransfer.Lines.SerialNumbers.Add();

                                    oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse;
                                    oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = nS;
                                    oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.fromBinEntry;
                                    oStockTransfer.Lines.BinAllocations.Quantity = 1;
                                    oStockTransfer.Lines.BinAllocations.Add();

                                    oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse;
                                    oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = nS;
                                    oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.toBinEntry;
                                    oStockTransfer.Lines.BinAllocations.Quantity = 1;
                                    oStockTransfer.Lines.BinAllocations.Add();
                                    sumQty = sumQty + serial.Quantity;
                                    nS++;
                                }

                            }                            
                            //==================================================


                            if ((l.InputQty - sumQty) == 0)
                            {
                                BalanceQty = 0;
                            }
                            else
                            {
                                BalanceQty = (l.InputQty - sumQty);
                            }

                            //oStockTransfer.Lines.UserFields.Fields.Item("U_BalanceQty").Value = BalanceQty.ToString();
                            lineNumb++;

                            oStockTransfer.Lines.Add();

                        }
                        //No B/SN
                        if(l.ProductType == "n")
                        {
                            oStockTransfer.Lines.SetCurrentLine(lineNumb);

                            oStockTransfer.Lines.BaseEntry = l.BaseEntry;
                            oStockTransfer.Lines.BaseLine = l.BaseLine;
                            oStockTransfer.Lines.BaseType = SAPbobsCOM.InvBaseDocTypeEnum.InventoryTransferRequest;

                            oStockTransfer.Lines.ItemCode = l.ItemCode;
                            oStockTransfer.Lines.FromWarehouseCode = l.FromWhsCode;
                            oStockTransfer.Lines.WarehouseCode = l.ToWhsCode;
                            oStockTransfer.Lines.Quantity = l.QtyInSap;

                            string strPatient = "";
                            string xPatient = "";
                            strPatient = l.U_Patient;
                            if (strPatient is not null)
                            {
                                xPatient = strPatient;
                            }
                            else
                            {
                                xPatient = " ";
                            }
                            oStockTransfer.Lines.UserFields.Fields.Item("U_Patient").Value = xPatient;
                                
                                oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse;
                                oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0;
                                oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.fromBinEntry;
                                oStockTransfer.Lines.BinAllocations.Quantity = l.Quantity;
                                oStockTransfer.Lines.BinAllocations.Add();

                                oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse;
                                oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0;
                                oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.toBinEntry;
                                oStockTransfer.Lines.BinAllocations.Quantity = l.Quantity;
                                oStockTransfer.Lines.BinAllocations.Add();

                                sumQty = sumQty + l.Quantity;
                            //==================================================


                            if ((l.InputQty - sumQty) == 0)
                            {
                                BalanceQty = 0;
                            }
                            else
                            {
                                BalanceQty = (l.InputQty - sumQty);
                            }

                            //oStockTransfer.Lines.UserFields.Fields.Item("U_BalanceQty").Value = BalanceQty.ToString();
                            lineNumb++;

                            oStockTransfer.Lines.Add();
                        }

                    }

                    oStockTransfer.Comments = "IFR:" + sendInventoryTransfer.U_loannum + " (" + sendInventoryTransfer.Comments + ")";

                    Retval = oStockTransfer.Add();
                    if (Retval != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);

                        return Task.FromResult(new ResponseInventoryTransfer
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = null
                        });
                    }
                    else
                    {
                        //Success
                        return Task.FromResult(new ResponseInventoryTransfer
                        {
                            ErrorCode = 0,
                            ErrorMsg = "",
                            DocEntry = oCompany.GetNewObjectKey(),
                        });

                    }

                }
                else
                {
                    return Task.FromResult(new ResponseInventoryTransfer
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseInventoryTransfer
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        public Task<ResponseInventoryTransfer> responseInventoryTransferCV(SendInventoryTransfer sendInventoryTransfer)
        {
            try
            {
                SAPbobsCOM.StockTransfer oStockTransfer;
                SAPbobsCOM.Company oCompany;
                int Retval = 0;
                int i = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    string strWebID = "";
                    string txt_U_pname = "";
                    var date = DateTime.Now;
                    strWebID = sendInventoryTransfer.Series + "-" + date.Day + "" + date.Month + "" + date.Year + "-" + date.Hour + "" + date.Minute + "" + date.Second;
                    double sumQty = 0;
                    double BalanceQty = 0;
                    oCompany = login.Company;

                    oStockTransfer = (StockTransfer)oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);

                    //Head
                    oStockTransfer.Series = sendInventoryTransfer.Series;
                    oStockTransfer.CardCode = sendInventoryTransfer.CardCode;
                    oStockTransfer.DocDate = sendInventoryTransfer.DocDate;
                    oStockTransfer.FromWarehouse = sendInventoryTransfer.FromWhsCode;
                    oStockTransfer.ToWarehouse = sendInventoryTransfer.ToWhsCode;
                    oStockTransfer.ShipToCode = sendInventoryTransfer.ShipToCode;
                    oStockTransfer.Address = sendInventoryTransfer.Address;
                    oStockTransfer.SalesPersonCode = sendInventoryTransfer.SalesEmployeeCode;

                    oStockTransfer.UserFields.Fields.Item("U_WebID").Value = strWebID;
                    oStockTransfer.UserFields.Fields.Item("U_loannum").Value = sendInventoryTransfer.U_loannum;
                    if (sendInventoryTransfer.U_pname == null || sendInventoryTransfer.U_pname == "")
                    {
                        txt_U_pname = " ";
                    }
                    else
                    {
                        txt_U_pname = sendInventoryTransfer.U_pname;
                    }
                    oStockTransfer.UserFields.Fields.Item("U_pname").Value = txt_U_pname;

                    string IMDocNum = "";
                    IMDocNum = sendInventoryTransfer.U_loannum;
                    //==== Line ====
                    int lineNumb = 0;
                    foreach (SendInventoryTransferLine l in sendInventoryTransfer.Line)
                    {                        

                        if (l.BatchLine != null || l.SerialLine != null)
                        {
                            sumQty = 0;
                            oStockTransfer.Lines.SetCurrentLine(lineNumb);
                            oStockTransfer.Lines.ItemCode = l.ItemCode;
                            oStockTransfer.Lines.FromWarehouseCode = l.FromWhsCode;
                            oStockTransfer.Lines.WarehouseCode = l.ToWhsCode;
                            oStockTransfer.Lines.Quantity = l.Quantity;
                            oStockTransfer.Lines.UoMEntry = l.UomEntry;
                            oStockTransfer.Lines.UseBaseUnits = BoYesNoEnum.tNO;

                            string strPatient = "";
                            string xPatient = "";
                            strPatient = l.U_Patient;
                            if (strPatient is not null)
                            {
                                xPatient = strPatient;
                            }
                            else
                            {
                                xPatient = " ";
                            }
                            oStockTransfer.Lines.UserFields.Fields.Item("U_Patient").Value = xPatient;

                            string strTranferNo = "";
                            string xTranferNo = "";
                            strTranferNo = l.U_TranferNo;
                            if (strTranferNo is not null)
                            {
                                xTranferNo = strTranferNo;
                            }
                            else
                            {
                                xTranferNo = " ";
                            }
                            oStockTransfer.Lines.UserFields.Fields.Item("U_TranferNo").Value = xTranferNo;

                            if (l.ProductType == "b" && l.BatchLine != null)
                            {
                                int nB = 0;
                                foreach (var batch in l.BatchLine)
                                {
                                    oStockTransfer.Lines.BatchNumbers.SetCurrentLine(nB);
                                    oStockTransfer.Lines.BatchNumbers.BatchNumber = batch.BatchNumber;
                                    oStockTransfer.Lines.BatchNumbers.Quantity = batch.Quantity;
                                    oStockTransfer.Lines.BatchNumbers.Add();

                                    oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse;
                                    oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = nB;
                                    oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.fromBinEntry;
                                    oStockTransfer.Lines.BinAllocations.Quantity = batch.Quantity;
                                    oStockTransfer.Lines.BinAllocations.Add();

                                    oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse;
                                    oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = nB;
                                    oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.toBinEntry;
                                    oStockTransfer.Lines.BinAllocations.Quantity = batch.Quantity;
                                    oStockTransfer.Lines.BinAllocations.Add();

                                    sumQty = sumQty + batch.Quantity;
                                    nB++;
                                }
                            }
                            else if (l.ProductType == "s" && l.SerialLine != null)
                            {
                                int nS = 0;
                                foreach (var serial in l.SerialLine)
                                {
                                    oStockTransfer.Lines.SerialNumbers.SetCurrentLine(nS);
                                    oStockTransfer.Lines.SerialNumbers.ManufacturerSerialNumber = serial.SerialNumber;
                                    oStockTransfer.Lines.SerialNumbers.InternalSerialNumber = serial.SerialNumber;
                                    oStockTransfer.Lines.SerialNumbers.Quantity = 1;
                                    oStockTransfer.Lines.SerialNumbers.Add();

                                    oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse;
                                    oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = nS;
                                    oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.fromBinEntry;
                                    oStockTransfer.Lines.BinAllocations.Quantity = 1;
                                    oStockTransfer.Lines.BinAllocations.Add();

                                    oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse;
                                    oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = nS;
                                    oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.toBinEntry;
                                    oStockTransfer.Lines.BinAllocations.Quantity = 1;
                                    oStockTransfer.Lines.BinAllocations.Add();
                                    sumQty = sumQty + serial.Quantity;
                                    nS++;
                                }

                            }
                            //==================================================


                            if ((l.InputQty - sumQty) == 0)
                            {
                                BalanceQty = 0;
                            }
                            else
                            {
                                BalanceQty = (l.InputQty - sumQty);
                            }

                            //oStockTransfer.Lines.UserFields.Fields.Item("U_BalanceQty").Value = BalanceQty.ToString();
                            lineNumb++;

                            oStockTransfer.Lines.Add();

                        }
                        //No B/SN
                        if (l.ProductType == "n")
                        {
                            oStockTransfer.Lines.SetCurrentLine(lineNumb);

                            oStockTransfer.Lines.ItemCode = l.ItemCode;
                            oStockTransfer.Lines.FromWarehouseCode = l.FromWhsCode;
                            oStockTransfer.Lines.WarehouseCode = l.ToWhsCode;
                            oStockTransfer.Lines.Quantity = l.Quantity;
                            oStockTransfer.Lines.UoMEntry = l.UomEntry;
                            oStockTransfer.Lines.UseBaseUnits = BoYesNoEnum.tNO;

                            string strPatient = "";
                            string xPatient = "";
                            strPatient = l.U_Patient;
                            if (strPatient is not null)
                            {
                                xPatient = strPatient;
                            }
                            else
                            {
                                xPatient = " ";
                            }
                            oStockTransfer.Lines.UserFields.Fields.Item("U_Patient").Value = xPatient;

                            oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse;
                            oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0;
                            oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.fromBinEntry;
                            oStockTransfer.Lines.BinAllocations.Quantity = l.Quantity;
                            oStockTransfer.Lines.BinAllocations.Add();

                            oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse;
                            oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0;
                            oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.toBinEntry;
                            oStockTransfer.Lines.BinAllocations.Quantity = l.Quantity;
                            oStockTransfer.Lines.BinAllocations.Add();

                            sumQty = sumQty + l.Quantity;
                            //==================================================


                            if ((l.InputQty - sumQty) == 0)
                            {
                                BalanceQty = 0;
                            }
                            else
                            {
                                BalanceQty = (l.InputQty - sumQty);
                            }

                            //oStockTransfer.Lines.UserFields.Fields.Item("U_BalanceQty").Value = BalanceQty.ToString();
                            lineNumb++;

                            oStockTransfer.Lines.Add();
                        }

                    }

                    oStockTransfer.Comments = "IM-" + sendInventoryTransfer.U_loannum + " (" + sendInventoryTransfer.Comments + ")";

                    Retval = oStockTransfer.Add();
                    if (Retval != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);
                        return Task.FromResult(new ResponseInventoryTransfer
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = null
                        });
                    }
                    else
                    {

                        sumQty = 0;
                        BalanceQty = 0;
                        oCompany = login.Company;
                        SAPbobsCOM.Recordset oRS = null;
                        oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        foreach (SendInventoryTransferLine l in sendInventoryTransfer.Line)
                        {
                            sumQty = 0;
                            int DocEntry = 0;
                            string ItemCode = "";
                            int LineNum = 0;
                            double iBalanceQty = 0;
                            string str_U_loannum, str_U_mStatus;
                            string param1 = "U_BalanceQty";
                            string param2 = "U_TranferNo";
                            string param3 = "U_mStatus";
                            string param4 = "DocEntry";
                            string param5 = "ItemCode";
                            string param6 = "LineNum";

                            if (l.ProductType == "b" && l.BatchLine != null)
                            {

                                foreach (var batch in l.BatchLine)
                                {
                                    sumQty = sumQty + batch.Quantity;
                                }

                            }
                            else if (l.ProductType == "s" && l.SerialLine != null)
                            {
                                foreach (var serial in l.SerialLine)
                                {
                                    sumQty = sumQty + serial.Quantity;
                                }
                            }
                            else if (l.ProductType == "n")
                            {
                                sumQty = sumQty + l.Quantity;
                            }

                            if ((l.TotalQty - sumQty) == 0)
                            {
                                BalanceQty = 0;
                                //str_U_loannum = ", \"" + param2 + "\" = " + sendInventoryTransfer.U_loannum + " ";
                                str_U_loannum = "";
                                str_U_mStatus = ", \"" + param3 + "\" = 'complete'";
                            }
                            else
                            {
                                BalanceQty = (l.TotalQty - sumQty);
                                str_U_loannum = "";
                                str_U_mStatus = ", \"" + param3 + "\" = '" + l.mStatus + "'";
                            }

                            DocEntry = l.BaseEntry;
                            ItemCode = l.ItemCode;
                            LineNum = l.BaseLine;
                            iBalanceQty = Convert.ToDouble(BalanceQty.ToString());



                            string UPD = "UPDATE \"" + ConnectionString.CompanyDB + "\".WTR1 SET " +
                                " \"" + param1 + "\" = " + iBalanceQty + " " + str_U_loannum + str_U_mStatus +
                                " WHERE \"" + param4 + "\"=" + DocEntry + " AND \"" + param5 + "\"='" + ItemCode + "' AND \"" + param6 + "\"=" + LineNum + " ";
                            oRS.DoQuery(UPD);
                        }


                        //Success
                        return Task.FromResult(new ResponseInventoryTransfer
                        {
                            ErrorCode = 0,
                            ErrorMsg = "",
                            DocEntry = oCompany.GetNewObjectKey(),
                        });

                    }

                }
                else
                {
                    return Task.FromResult(new ResponseInventoryTransfer
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseInventoryTransfer
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        public Task<ResponseInventoryTransfer> responseInventoryTransferIM(SendInventoryTransfer sendInventoryTransfer)
        {
            try
            {
                SAPbobsCOM.StockTransfer oStockTransfer;
                SAPbobsCOM.Company oCompany;
                int Retval = 0;
                int i = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    string strWebID = "";
                    string txt_U_pname = "";
                    var date = DateTime.Now;
                    //strWebID = sendInventoryTransfer.Series + "-" + date.Day + "" + date.Month + "" + date.Year + "-" + date.Hour + "" + date.Minute + "" + date.Second;
                    double sumQty = 0;
                    double BalanceQty = 0;
                    oCompany = login.Company;

                    oStockTransfer = (StockTransfer)oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);

                    //Head
                    oStockTransfer.Series = sendInventoryTransfer.Series;
                    oStockTransfer.CardCode = sendInventoryTransfer.CardCode;
                    oStockTransfer.DocDate = sendInventoryTransfer.DocDate;
                    oStockTransfer.FromWarehouse = sendInventoryTransfer.FromWhsCode;
                    oStockTransfer.ToWarehouse = sendInventoryTransfer.ToWhsCode;

                    //START Line
                    int rLine = 0;
                    foreach (SendInventoryTransferLine l in sendInventoryTransfer.Line)
                    {                        
                        if (l.BatchLine.Count != 0 || l.SerialLine.Count != 0)
                        {
                            oStockTransfer.Lines.SetCurrentLine(rLine);

                            oStockTransfer.Lines.BaseEntry = l.BaseEntry;
                            oStockTransfer.Lines.BaseLine = l.BaseLine;
                            oStockTransfer.Lines.BaseType = SAPbobsCOM.InvBaseDocTypeEnum.InventoryTransferRequest;

                            oStockTransfer.Lines.ItemCode = l.ItemCode;
                            oStockTransfer.Lines.FromWarehouseCode = l.FromWhsCode;
                            oStockTransfer.Lines.WarehouseCode = l.ToWhsCode;
                            oStockTransfer.Lines.Quantity = l.QtyInSap;

                            if (l.ProductType == "b" && l.BatchLine != null)
                            {
                                int nB = 0;
                                foreach (var batch in l.BatchLine)
                                {
                                    oStockTransfer.Lines.BatchNumbers.SetCurrentLine(nB);
                                    oStockTransfer.Lines.BatchNumbers.BatchNumber = batch.BatchNumber;
                                    oStockTransfer.Lines.BatchNumbers.Quantity = batch.Quantity;
                                    oStockTransfer.Lines.BatchNumbers.Add();

                                    oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse;
                                    oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = nB;
                                    oStockTransfer.Lines.BinAllocations.BinAbsEntry = batch.fromBinEntryX;
                                    oStockTransfer.Lines.BinAllocations.Quantity = batch.Quantity;
                                    oStockTransfer.Lines.BinAllocations.Add();

                                    oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse;
                                    oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = nB;
                                    oStockTransfer.Lines.BinAllocations.BinAbsEntry = l.toBinEntry;
                                    oStockTransfer.Lines.BinAllocations.Quantity = batch.Quantity;
                                    oStockTransfer.Lines.BinAllocations.Add();

                                    sumQty = sumQty + batch.Quantity;
                                    nB++;
                                }
                            }



                            oStockTransfer.Lines.Add();
                            rLine++;
                        }

                        
                    }
                    
                    //END Line

                    Retval = oStockTransfer.Add();
                    if (Retval != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);

                        return Task.FromResult(new ResponseInventoryTransfer
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = null
                        });
                    }
                    else
                    {
                        //Success
                        return Task.FromResult(new ResponseInventoryTransfer
                        {
                            ErrorCode = 0,
                            ErrorMsg = "",
                            DocEntry = oCompany.GetNewObjectKey(),
                        });

                    }
                }
                else
                {
                    return Task.FromResult(new ResponseInventoryTransfer
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseInventoryTransfer
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }


    }
}

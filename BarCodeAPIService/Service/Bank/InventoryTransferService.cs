using BarCodeAPIService.Connection;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class InventoryTransferService : IInventoryTransferService
    {
        private int ErrCode;
        private string ErrMsg;

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
                    oCompany = login.Company;

                    oStockTransfer = (SAPbobsCOM.StockTransfer)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer);
                    oStockTransfer.Series = sendInventoryTransfer.Series;
                    //oStockTransfer.DocNum = sendInventoryTransfer.DocNum;
                    //oStockTransfer.DocEntry = sendInventoryTransfer.DocEntry;
                    oStockTransfer.CardCode = sendInventoryTransfer.CardCode;
                    oStockTransfer.DocDate = sendInventoryTransfer.DocDate;
                    oStockTransfer.TaxDate = sendInventoryTransfer.TaxDate;
                    oStockTransfer.Comments = sendInventoryTransfer.Comments;
                    oStockTransfer.JournalMemo = sendInventoryTransfer.JournalRemark;
                    oStockTransfer.FromWarehouse = sendInventoryTransfer.FromWhsCode;
                    oStockTransfer.ToWarehouse = sendInventoryTransfer.ToWhsCode;

                    foreach (SendInventoryTransferLine l in sendInventoryTransfer.Line)
                    {
                            oStockTransfer.Lines.ItemCode = l.ItemCode;
                            oStockTransfer.Lines.Quantity = l.Quantity;
                            oStockTransfer.Lines.FromWarehouseCode = l.FromWhsCode;
                            oStockTransfer.Lines.WarehouseCode = l.ToWhsCode;
                            oStockTransfer.Lines.UserFields.Fields.Item("U_TranferNo").Value = l.U_TranferNo;

                        if (l.BatchNo != "")
                            {
                                oStockTransfer.Lines.BatchNumbers.SetCurrentLine(0);
                                oStockTransfer.Lines.BatchNumbers.BatchNumber = l.BatchNo;
                                oStockTransfer.Lines.BatchNumbers.Quantity = l.Quantity;
                                oStockTransfer.Lines.BatchNumbers.Add();
                            }
                            else
                            {
                                if (l.SerialNo != "")
                                {
                                    oStockTransfer.Lines.SerialNumbers.SetCurrentLine(0);
                                    oStockTransfer.Lines.SerialNumbers.ManufacturerSerialNumber = l.SerialNo;
                                    oStockTransfer.Lines.SerialNumbers.Quantity = l.Quantity;
                                    oStockTransfer.Lines.SerialNumbers.Add();
                                }
                            }

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
                        
                        oStockTransfer.Lines.Add();
                    }
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

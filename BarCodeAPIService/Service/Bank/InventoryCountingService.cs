using BarCodeAPIService.Connection;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class InventoryCountingService : IInventoryCountingService
    {
        private int ErrCode;
        private string ErrMsg;

        public Task<ResponseInventoryCounting> responseInventoryCounting(SendInventoryCounting sendInventoryCounting)
        {
            try
            {
                SAPbobsCOM.Company oCompany;
                int Retval = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.CompanyService oCS = (SAPbobsCOM.CompanyService)oCompany.GetCompanyService();
                    SAPbobsCOM.InventoryCountingsService oICS = (SAPbobsCOM.InventoryCountingsService)oCS.GetBusinessService(SAPbobsCOM.ServiceTypes.InventoryCountingsService);
                    SAPbobsCOM.InventoryCounting oIC = (SAPbobsCOM.InventoryCounting)oICS.GetDataInterface(SAPbobsCOM.InventoryCountingsServiceDataInterfaces.icsInventoryCounting);

                    DateTime dt = DateTime.Now;

                    var dateString = sendInventoryCounting.CountingDate;
                    DateTime countdate = DateTime.ParseExact(dateString, "yyyy-MM-dd", null);

                    var timeString = sendInventoryCounting.CountingTime;
                    DateTime counttime = DateTime.ParseExact(timeString.PadLeft(4, '0'), "HH:mm", null);

                    oIC.CountDate = countdate;
                    oIC.CountTime = counttime;
                    oIC.Reference2 = sendInventoryCounting.Ref2;
                    oIC.Remarks = sendInventoryCounting.Comments;
                    oIC.SingleCounterID = Convert.ToInt32(sendInventoryCounting.CountingType);
                    oIC.UserFields.Item("U_C_binlo").Value = sendInventoryCounting.Udf_BinLocation;
                    //Counter=36

                    foreach (SendInventoryCountingLine l in sendInventoryCounting.Line)
                    {

                        SAPbobsCOM.InventoryCountingLines oICLS = oIC.InventoryCountingLines;
                        SAPbobsCOM.InventoryCountingLine oICL = oICLS.Add();
                        oICL.ItemCode = l.ItemCode;
                        oICL.CountedQuantity = l.Quantity;
                        oICL.WarehouseCode = l.WhsCode;
                        oICL.BinEntry = l.BinEntry;
                        oICL.UoMCode = l.UomCode;
                        oICL.Counted = SAPbobsCOM.BoYesNoEnum.tYES;

                        if (l.ProductType == "b" && l.BatchLine != null)
                        {
                            
                            foreach (var batch in l.BatchLine)
                            {
                                SAPbobsCOM.InventoryCountingBatchNumber oInventoryCountingBatchNumber = oICL.InventoryCountingBatchNumbers.Add();
                                oInventoryCountingBatchNumber.BatchNumber = batch.BatchNumber;
                                oInventoryCountingBatchNumber.Quantity = batch.Quantity;
                            }
                                
                        }
                        else if (l.ProductType == "s" && l.SerialLine != null)
                        {
                            foreach (var serial in l.SerialLine)
                            {
                                SAPbobsCOM.InventoryCountingSerialNumber oInventoryCountingSerialNumber = oICL.InventoryCountingSerialNumbers.Add();
                                oInventoryCountingSerialNumber.InternalSerialNumber = serial.SerialNumber;
                                //oInventoryCountingSerialNumber.ManufacturerSerialNumber = serial.SerialNumber;
                                oInventoryCountingSerialNumber.Quantity = serial.Quantity;
                            }                            
                        }
       
                    }

                    SAPbobsCOM.InventoryCountingParams oInventoryCountingParams = oICS.Add(oIC);
                    //Retval = oICS.Add();

                    if (Retval != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);
                        return Task.FromResult(new ResponseInventoryCounting
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = null
                        });
                    }
                    else
                    {
                        return Task.FromResult(new ResponseInventoryCounting
                        {
                            ErrorCode = 0,
                            ErrorMsg = "",
                            DocEntry = oCompany.GetNewObjectKey(),
                        });
                    }

                }
                else
                {
                    return Task.FromResult(new ResponseInventoryCounting
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseInventoryCounting
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }

        }

    }
}


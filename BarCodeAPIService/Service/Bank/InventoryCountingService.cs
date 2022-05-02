using System;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using SAPbobsCOM;

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
                Company oCompany;

                var Retval = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;

                    var oCS = oCompany.GetCompanyService();
                    var oICS = (InventoryCountingsService)oCS.GetBusinessService(ServiceTypes
                        .InventoryCountingsService);
                    var oIC = (InventoryCounting)oICS.GetDataInterface(InventoryCountingsServiceDataInterfaces
                        .icsInventoryCounting);

                    var dt = DateTime.Now;

                    var dateString = sendInventoryCounting.CountingDate;
                    var countdate = DateTime.ParseExact(dateString, "yyyy-MM-dd", null);
                    var timeString = sendInventoryCounting.CountingTime;
                    var counttime = DateTime.ParseExact(timeString, "HH:mm", null);

                    oIC.CountDate = countdate;
                    oIC.CountTime = counttime;
                    oIC.Reference2 = sendInventoryCounting.Ref2;
                    oIC.Remarks = sendInventoryCounting.Comments;
                    oIC.SingleCounterID = Convert.ToInt32(sendInventoryCounting.CountingType);
                    //Counter=36

                    foreach (var l in sendInventoryCounting.Line)
                    {
                        var oICLS = oIC.InventoryCountingLines;
                        var oICL = oICLS.Add();
                        oICL.ItemCode = l.ItemCode;
                        oICL.CountedQuantity = l.CountedQuantity;
                        oICL.WarehouseCode = l.WhsCode;
                        oICL.BinEntry = l.BinEntry;
                        oICL.UoMCode = l.UomCode;
                        oICL.Counted = BoYesNoEnum.tYES;

                        if (l.BatchNo != "")
                        {
                            var oInventoryCountingBatchNumber = oICL.InventoryCountingBatchNumbers.Add();
                            oInventoryCountingBatchNumber.BatchNumber = l.BatchNo;
                            oInventoryCountingBatchNumber.Quantity = l.Quantity;
                        }
                        else
                        {
                            if (l.SerialNo != "")
                            {
                                var oInventoryCountingSerialNumber = oICL.InventoryCountingSerialNumbers.Add();
                                oInventoryCountingSerialNumber.InternalSerialNumber = l.SerialNo;
                                oInventoryCountingSerialNumber.ManufacturerSerialNumber = l.SerialNo;
                                oInventoryCountingSerialNumber.Quantity = l.Quantity;
                            }
                        }

                        //foreach (SendInventoryCountingBatch b in sendInventoryCounting.BatchLine)
                        //{
                        //    SAPbobsCOM.InventoryCountingBatchNumber oInventoryCountingBatchNumber = oICL.InventoryCountingBatchNumbers.Add();
                        //    oInventoryCountingBatchNumber.BatchNumber = b.BatchNumber;
                        //    oInventoryCountingBatchNumber.Quantity = b.Quantity;
                        //}
                        //foreach (SendInventoryCountingSerial s in sendInventoryCounting.SerialLine)
                        //{
                        //    SAPbobsCOM.InventoryCountingSerialNumber oInventoryCountingSerialNumber = oICL.InventoryCountingSerialNumbers.Add();
                        //    oInventoryCountingSerialNumber.InternalSerialNumber = s.SerialNumber;
                        //    oInventoryCountingSerialNumber.ManufacturerSerialNumber = s.SerialNumber;
                        //    oInventoryCountingSerialNumber.Quantity = s.Quantity;
                        //}
                    }

                    var oInventoryCountingParams = oICS.Add(oIC);
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

                    return Task.FromResult(new ResponseInventoryCounting
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        DocEntry = oCompany.GetNewObjectKey()
                    });
                }

                return Task.FromResult(new ResponseInventoryCounting
                {
                    ErrorCode = login.LErrCode,
                    ErrorMsg = login.SErrMsg
                });
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
using BarCodeAPIService.Connection;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                SAPbobsCOM.Documents Delivery;
                SAPbobsCOM.Company oCompany;
                int Retval = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Delivery = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDeliveryNotes);
                    Delivery.CardCode = sendDelivery.CardCode;
                    Delivery.NumAtCard = sendDelivery.NumAtCard;
                    Delivery.DocDate = sendDelivery.DocDate;
                    Delivery.ContactPersonCode = sendDelivery.ContactPersion;
                   // Delivery.DocType = sendDelivery.DocType;

                    foreach (SendDeliveryLine l in sendDelivery.Lines)
                    {
                        Delivery.Lines.ItemCode = l.ItemCode;
                        Delivery.Lines.Quantity = l.Quantity;
                        Delivery.Lines.UnitPrice = l.UnitPrice;
                        Delivery.Lines.DiscountPercent = l.Discount;
                        Delivery.Lines.TaxCode = l.TaxCode;
                        Delivery.Lines.WarehouseCode = l.WarehouseCode;
                        Delivery.Lines.NCMCode = l.UomCode;
                        Delivery.Lines.Add();
                    }
                    Retval = Delivery.Add();
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
                    else
                    {
                        return Task.FromResult(new ResponseDelivery
                        {
                            ErrorCode = 0,
                            ErrorMsg = "",
                            DocEntry = oCompany.GetNewObjectKey(),
                        });
                    }

                }
                else
                {
                    return Task.FromResult(new ResponseDelivery
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg
                    });
                }
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

        public Task<ResponseGetORDR> responseGetORDR()
        {
            var oRDRs = new List<ORDR>();
            var rDR1s = new List<RDR1>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_SOUNTITYA('ORDR','','','','','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        //oRSLine.DoQuery("EXEC USP_Get_Transcation_Data 'PDN1','"+ oRS.Fields.Item(0).Value+"','','','',''");
                        oRSLine.DoQuery("CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_SOUNTITYA('RDR1','" + oRS.Fields.Item(0).Value + "','','','','')");
                        rDR1s = new List<RDR1>();
                        while (!oRSLine.EoF)
                        {
                            rDR1s.Add(new RDR1
                            {
                                ItemCode = oRSLine.Fields.Item(0).Value.ToString(),
                                Description = oRSLine.Fields.Item(1).Value.ToString(),
                                Quatity = Convert.ToInt32(oRSLine.Fields.Item(2).Value),
                                Price = Convert.ToDouble(oRSLine.Fields.Item(3).Value),
                                DiscPrcnt = Convert.ToDouble(oRSLine.Fields.Item(4).Value),
                                VatGroup = oRSLine.Fields.Item(5).Value.ToString(),
                                LineTotal = Convert.ToDouble(oRSLine.Fields.Item(6).Value),
                                WhsCode = oRSLine.Fields.Item(6).Value.ToString(),
                            });
                            oRSLine.MoveNext();
                        }
                        oRDRs.Add(new ORDR
                        {
                            CardCode = oRS.Fields.Item(1).Value.ToString(),
                            CardName = oRS.Fields.Item(2).Value.ToString(),
                            CntctCode = Convert.ToInt32(oRS.Fields.Item(3).Value.ToString()),
                            NumAtCard = oRS.Fields.Item(4).Value.ToString(),
                            DocStatus = oRS.Fields.Item(5).Value.ToString(),
                            Line = rDR1s.ToList()
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseGetORDR
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oRDRs.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseGetORDR
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
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
    }
}

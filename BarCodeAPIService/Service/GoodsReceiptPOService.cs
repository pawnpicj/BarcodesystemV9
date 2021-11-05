using BarCodeAPIService.Connection;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class GoodsReceiptPOService : IGoodsReceiptPOService
    {
        private int ErrCode;
        private string ErrMsg;

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
                            }) ;
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

        public Task<ResponseOPDNGetPO> responseOPDNGetPO()
        {
            var oPDNs = new List<OPDN>();
            var pDN1s = new List<PDN1>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_TENGKIMLEANG('OPDN','','','','','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        //oRSLine.DoQuery("EXEC USP_Get_Transcation_Data 'PDN1','"+ oRS.Fields.Item(0).Value+"','','','',''");
                        oRSLine.DoQuery("CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_TENGKIMLEANG('PDN1','" + oRS.Fields.Item(0).Value + "','','','','')");
                        pDN1s = new List<PDN1>();
                        while (!oRSLine.EoF)
                        {
                            pDN1s.Add(new PDN1
                            {
                                ItemCode=oRSLine.Fields.Item(0).Value.ToString(),
                                Description=oRSLine.Fields.Item(1).Value.ToString(),
                                Quatity=Convert.ToInt32(oRSLine.Fields.Item(2).Value),
                                Price=Convert.ToDouble(oRSLine.Fields.Item(3).Value),
                                DiscPrcnt=Convert.ToDouble(oRSLine.Fields.Item(4).Value),
                                VatGroup=oRSLine.Fields.Item(5).Value.ToString(),
                                LineTotal= Convert.ToDouble(oRSLine.Fields.Item(6).Value),
                                WhsCode=oRSLine.Fields.Item(6).Value.ToString(),
                            });
                            oRSLine.MoveNext();
                        }
                        oPDNs.Add(new OPDN
                        {
                            CardCode = oRS.Fields.Item(1).Value.ToString(),
                            CardName = oRS.Fields.Item(2).Value.ToString(),
                            CntctCode = Convert.ToInt32(oRS.Fields.Item(3).Value.ToString()),
                            NumAtCard = oRS.Fields.Item(4).Value.ToString(),
                            Line = pDN1s.ToList()
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOPDNGetPO
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oPDNs.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOPDNGetPO
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }catch(Exception ex)
            {
                return Task.FromResult(new ResponseOPDNGetPO
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

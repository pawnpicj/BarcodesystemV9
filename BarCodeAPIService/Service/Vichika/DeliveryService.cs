using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using SAPbobsCOM;

namespace BarCodeAPIService.Service
{
    public class DeliveryService : IDeliveryService
    {
        public Task<ResponseDelivery> PostDelivery(SendDelivery sendDelivery)
        {
            throw new NotImplementedException();
        }

        //private int ErrCode;
        //private string ErrMsg;


        //public Task<ResponseDelivery> PostDelivery(SendDelivery sendDelivery)
        //{

        //    try
        //    {
        //        SAPbobsCOM.Documents oDelivery;
        //        SAPbobsCOM.Company oCompany;
        //        int Retval = 0;
        //        Login login = new();
        //        if (login.LErrCode == 0)
        //        {
        //            oCompany = login.Company;
        //            oDelivery = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDeliveryNotes);
        //            oDelivery.CardCode = sendDelivery.CardCode;
        //            oDelivery.NumAtCard = sendDelivery.NumAtCard;
        //            oDelivery.DocDate = sendDelivery.DocDate;
        //            oDelivery.ContactPersonCode = sendDelivery.ContactPersion;
        //            //oDelivery.DocType = sendDelivery.DocType;

        //            foreach (SendDeliveryLine l in sendDelivery.Lines)
        //            {
        //                oDelivery.Lines.ItemCode = l.ItemCode;
        //                oDelivery.Lines.Quantity = l.Quantity;
        //                oDelivery.Lines.UnitPrice = l.UnitPrice;
        //                oDelivery.Lines.DiscountPercent = l.Discount;
        //                oDelivery.Lines.TaxCode = l.TaxCode;
        //                oDelivery.Lines.WarehouseCode = l.WarehouseCode;
        //                //oDelivery.Lines.NCMCode = l.UomCode;                        
        //                oDelivery.Lines.Add();
        //            }
        //            Retval = oDelivery.Add();
        //            if (Retval != 0)
        //            {
        //                oCompany.GetLastError(out ErrCode, out ErrMsg);
        //                return Task.FromResult(new ResponseDelivery
        //                {
        //                    ErrorCode = ErrCode,
        //                    ErrorMsg = ErrMsg,
        //                    DocEntry = null
        //                });
        //            }
        //            else
        //            {
        //                return Task.FromResult(new ResponseDelivery
        //                {
        //                    ErrorCode = 0,
        //                    ErrorMsg = "",
        //                    DocEntry = oCompany.GetNewObjectKey(),
        //                });
        //            }

        //        }
        //        else
        //        {
        //            return Task.FromResult(new ResponseDelivery
        //            {
        //                ErrorCode = login.LErrCode,
        //                ErrorMsg = login.SErrMsg
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.FromResult(new ResponseDelivery
        //        {
        //            ErrorCode = ex.HResult,
        //            ErrorMsg = ex.Message
        //        });
        //    }
        // }

        public Task<ResponseGetORDR> responseGetORDR()
        {
            var oRDR = new List<ORDR>();
            var lRDR1 = new List<RDR1>();
            Company oCompany;

            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset oRS = null;
                    Recordset? oRSLine = null;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    var Query = "CALL \"" + ConnectionString.CompanyDB +
                                "\"._USP_CALLTRANS_SOUNTITYA ('ORDR','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        //Line
                        var QueryLine =
                            $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_SOUNTITYA ('RDR1','{oRS.Fields.Item(1).Value}','','','','')";
                        oRSLine.DoQuery(QueryLine);
                        lRDR1 = new List<RDR1>();
                        while (!oRSLine.EoF)
                        {
                            lRDR1.Add(new RDR1
                            {
                                //ItemCode = oRSLine.Fields.Item(1).Value.ToString(),
                                //Dscription = oRSLine.Fields.Item(2).Value.ToString(),
                                //Quantity = Convert.ToInt32(oRSLine.Fields.Item(3).Value),
                                //UomCode = oRSLine.Fields.Item(6).Value.ToString(),
                                //unitMsr = oRSLine.Fields.Item(7).Value.ToString(),
                                //U_unitprice = Convert.ToDouble(oRSLine.Fields.Item(8).Value)
                            });
                            oRSLine.MoveNext();
                        }
                        //End Line

                        // Head
                        oRDR.Add(new ORDR
                        {
                            DocNum = oRS.Fields.Item(0).Value.ToString(),
                            DocEntry = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            CardName = oRS.Fields.Item(2).Value.ToString(),
                            CardCode = oRS.Fields.Item(3).Value.ToString(),
                            CntctCode = Convert.ToInt32(oRS.Fields.Item(4).Value.ToString()),
                            NumAtCard = oRS.Fields.Item(5).Value.ToString(),
                            DocStatus = oRS.Fields.Item(6).Value.ToString(),
                            DocDate = oRS.Fields.Item(7).Value.ToString(),
                            DocDueDate = Convert.ToDateTime(oRS.Fields.Item(8).Value.ToString()),
                            TaxDate = Convert.ToDateTime(oRS.Fields.Item(9).Value.ToString()),
                            DocTotal = Convert.ToDouble(oRS.Fields.Item(10).Value.ToString()),
                            DiscPrcnt = Convert.ToDouble(oRS.Fields.Item(11).Value.ToString()),
                            SlpCode = Convert.ToInt32(oRS.Fields.Item(12).Value.ToString()),
                            SlpName = oRS.Fields.Item(13).Value.ToString()
                        });
                        oRS.MoveNext();
                        //DocDate = Convert.ToDateTime(oRS.Fields.Item(2).Value.ToString()),
                    }

                    return Task.FromResult(new ResponseGetORDR
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oRDR.ToList()
                    });
                }

                return Task.FromResult(new ResponseGetORDR
                {
                    ErrorCode = login.LErrCode,
                    ErrorMessage = login.SErrMsg,
                    Data = null
                });
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

        public Task<ResponseGetORDRLine> responseGetORDRLine(int DocEntry)
        {
            var getRDRLine = new List<ORDRLine>();
            Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    Recordset? oRS = null;
                    Recordset? oRSLine = null;
                    var sqlStr =
                        $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_SOUNTITYA ('RDR1','{DocEntry}','','','','')";
                    ;
                    oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRSLine = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getRDRLine.Add(new ORDRLine
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            Quatity = Convert.ToDouble(oRS.Fields.Item(2).Value.ToString()),
                            Price = Convert.ToDouble(oRS.Fields.Item(3).Value.ToString()),
                            DiscPrcnt = Convert.ToDouble(oRS.Fields.Item(4).Value.ToString()),
                            VatGroup = oRS.Fields.Item(5).Value.ToString(),
                            LineTotal = Convert.ToDouble(oRS.Fields.Item(6).Value.ToString()),
                            WhsCode = oRS.Fields.Item(7).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseGetORDRLine
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        Data = getRDRLine
                    });
                }

                return Task.FromResult(new ResponseGetORDRLine
                {
                    ErrorCode = login.LErrCode,
                    ErrorMsg = login.SErrMsg,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetORDRLine
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message,
                    Data = null
                });
            }
        }
    }
}
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
    public class DeliveryService : IDeliveryService
    {
        private int ErrCode;
        private string ErrMsg;

        public Task<ResponseDelivery> PostDelivery(SendDelivery sendDelivery)
        {
            
            try
            {
                SAPbobsCOM.Documents oDelivery;
                SAPbobsCOM.Company oCompany;
                int Retval = 0;
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    oDelivery = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDeliveryNotes);
                    oDelivery.CardCode = sendDelivery.CardCode;
                    oDelivery.NumAtCard = sendDelivery.NumAtCard;
                    oDelivery.DocDate = sendDelivery.DocDate;
                    oDelivery.ContactPersonCode = sendDelivery.ContactPersion;
                    //oDelivery.DocType = sendDelivery.DocType;

                    foreach (SendDeliveryLine l in sendDelivery.Lines)
                    {
                        oDelivery.Lines.ItemCode = l.ItemCode;
                        oDelivery.Lines.Quantity = l.Quantity;
                        oDelivery.Lines.UnitPrice = l.UnitPrice;
                        oDelivery.Lines.DiscountPercent = l.Discount;
                        oDelivery.Lines.TaxCode = l.TaxCode;
                        oDelivery.Lines.WarehouseCode = l.WarehouseCode;
                        //oDelivery.Lines.NCMCode = l.UomCode;                        
                        oDelivery.Lines.Add();
                    }
                    Retval = oDelivery.Add();
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
            DataTable dt = new DataTable();
            DataTable dtline = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {                   
                    string query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_SOUNTITYA('ORDR','','','','','')"; ;
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(query, login.CN);
                    login.AD.Fill(dt);
                    foreach(DataRow row in dt.Rows)
                    {
                        dtline = new DataTable();
                        string query1 = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_SOUNTITYA('RDR1','" + row["DocEntry"].ToString() + "','','','','')";
                        login.AD = new System.Data.Odbc.OdbcDataAdapter(query1, login.CN);
                        login.AD.Fill(dtline);
                        rDR1s = new List<RDR1>();                        
                        foreach (DataRow rowline in dtline.Rows)
                        {
                            rDR1s.Add(new RDR1
                            {
                                
                                Description =rowline["Dscription"].ToString(),
                                DiscPrcnt = Convert.ToDouble(rowline["DiscPrcnt"].ToString()),
                                ItemCode = rowline["ItemCode"].ToString(),
                                Quatity = Convert.ToDouble(rowline["Quantity"].ToString()),
                                Price = Convert.ToDouble(rowline["Price"].ToString()),                                
                                VatGroup = rowline["VatGroup"].ToString(),
                                LineTotal = Convert.ToDouble(rowline["LineTotal"].ToString()),
                                WhsCode = rowline["WhsCode"].ToString()
                            });                           
                        }
                        oRDRs.Add(new ORDR
                        {
                            CardName = row["CardName"].ToString(),
                            CardCode = row["CardCode"].ToString(),                         
                            CntctCode = Convert.ToInt32(row["CntctCode"].ToString()),
                            NumAtCard = row["NumAtCard"].ToString(),
                            DocNum = row["DocNum"].ToString(),
                            DocStatus = row["DocStatus"].ToString(),
                            Line = rDR1s.ToList(),
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
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetORDR
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return Task.FromResult(new ResponseGetORDR
            {
                Data = oRDRs,
                ErrorCode = 0,
                ErrorMessage = ""
            });
        }
    }
}

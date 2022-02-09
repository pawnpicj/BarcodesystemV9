using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using BarCodeAPIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace BarCodeAPIService.Service
{
    public class InventoryTransferRequestService : IInventoryTransferRequestService
    {
        public Task<ResponseGetOWTQ> responseGetOWTQ()
        {
            var oWTQ = new List<OWTQ>();
            var lWTQ1 = new List<WTQ1>();
            SAPbobsCOM.Company oCompany;

            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_BANK ('OWTQ','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        //Line
                        string QueryLine = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('WTQ1','{oRS.Fields.Item(1).Value}','','','','')";
                        oRSLine.DoQuery(QueryLine);
                        lWTQ1 = new List<WTQ1>();
                        while (!oRSLine.EoF)
                        {
                            lWTQ1.Add(new WTQ1
                            {
                                ItemCode = oRSLine.Fields.Item(1).Value.ToString(),
                                Dscription = oRSLine.Fields.Item(2).Value.ToString(),
                                Quantity = Convert.ToInt32(oRSLine.Fields.Item(3).Value),
                                UomCode = oRSLine.Fields.Item(6).Value.ToString(),
                                unitMsr = oRSLine.Fields.Item(7).Value.ToString(),
                                U_unitprice = Convert.ToDouble(oRSLine.Fields.Item(8).Value)
                            });
                            oRSLine.MoveNext();
                        }
                        //End Line

                        // Head
                        oWTQ.Add(new OWTQ
                        {
                            DocNum = Convert.ToInt32(oRS.Fields.Item(0).Value.ToString()),
                            DocEntry = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            DocDate = oRS.Fields.Item(2).Value.ToString(),
                            CardCode = oRS.Fields.Item(3).Value.ToString(),
                            CardName = oRS.Fields.Item(4).Value.ToString(),
                            SlpCode = oRS.Fields.Item(5).Value.ToString(),
                            SlpName = oRS.Fields.Item(6).Value.ToString(),
                            FromWhs = oRS.Fields.Item(7).Value.ToString(),
                            ToWhs = oRS.Fields.Item(8).Value.ToString(),
                            SeriesName = oRS.Fields.Item(9).Value.ToString()
                        });
                        oRS.MoveNext();
                        //DocDate = Convert.ToDateTime(oRS.Fields.Item(2).Value.ToString()),
                    }
                    return Task.FromResult(new ResponseGetOWTQ
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        Data = oWTQ.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseGetOWTQ
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetOWTQ
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponseGetWTQLine> responseGetWTQLine(int DocEntry)
        {
            var getWTQLine = new List<WTQLine>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    SAPbobsCOM.Recordset? oRSLine = null;
                    string sqlStr = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_BANK ('WTQ1','{DocEntry}','','','','')"; ;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRSLine = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        getWTQLine.Add(new WTQLine
                        {
                            DocEntry = Convert.ToInt32(oRS.Fields.Item(0).Value.ToString()),
                            ItemCode = oRS.Fields.Item(1).Value.ToString(),
                            Dscription = oRS.Fields.Item(2).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(3).Value.ToString()),
                            FromWhsCod = oRS.Fields.Item(4).Value.ToString(),
                            WhsCode = oRS.Fields.Item(5).Value.ToString(),
                            UomCode = oRS.Fields.Item(6).Value.ToString(),
                            unitMsr = oRS.Fields.Item(7).Value.ToString(),
                            U_unitprice = Convert.ToDouble(oRS.Fields.Item(8).Value.ToString())

                            //OnHand = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            //SerailNumber = oRS.Fields.Item(2).Value.ToString(),
                            //BatchNumber = oRS.Fields.Item(3).Value.ToString()
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseGetWTQLine
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        Data = getWTQLine
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseGetWTQLine
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetWTQLine
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message,
                    Data = null
                });
            }

        }
    }
}

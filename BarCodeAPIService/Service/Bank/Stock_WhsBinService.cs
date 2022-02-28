using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Bank;

namespace BarCodeAPIService.Service.Bank
{
    public class Stock_WhsBinService : IStock_WhsBinService
    {
        public Task<ResponseGetStockByWhsBin> responseGetStockByWhsBin()
        {
            var oLineStock = new List<LineStock>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_BANK ('GETSTOCKWB','01','01-A09-02-A62','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        oLineStock.Add(new LineStock
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            WhsCode = oRS.Fields.Item(2).Value.ToString(),
                            WhsName = oRS.Fields.Item(3).Value.ToString(),
                            BinCode = oRS.Fields.Item(4).Value.ToString(),
                            BinEntry = Convert.ToInt32(oRS.Fields.Item(5).Value.ToString()),
                            BatchNo = oRS.Fields.Item(6).Value.ToString(),
                            LotNumber = oRS.Fields.Item(7).Value.ToString(),
                            Quantity = Convert.ToDouble(oRS.Fields.Item(8).Value.ToString()),
                            UOMEntry = Convert.ToInt32(oRS.Fields.Item(9).Value.ToString()),
                            UOMCode = oRS.Fields.Item(10).Value.ToString(),
                            ExpDate = Convert.ToDateTime(oRS.Fields.Item(11).Value.ToString())

                            //BarCode = oRS.Fields.Item(3).Value.ToString(),
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseGetStockByWhsBin
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        Data = oLineStock.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseGetStockByWhsBin
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMsg = login.SErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseGetStockByWhsBin
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message,
                    Data = null
                });
            }
        }
    }
}

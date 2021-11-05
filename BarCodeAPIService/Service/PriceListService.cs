using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class PriceListService : IPriceListService
    {
        public Task<ResponseITM1GetPriceList> ResponseITM1GetPriceList()
        {
            var iTM1 = new List<ITM1>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string query = "";
                    oRS.DoQuery(query);
                    while (!oRS.EoF)
                    {
                        iTM1.Add(new ITM1
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            PriceList =Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            Price =Convert.ToDouble(oRS.Fields.Item(2).Value.ToString()),
                            ListName=oRS.Fields.Item(3).Value.ToString()
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseITM1GetPriceList
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = iTM1.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseITM1GetPriceList
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseITM1GetPriceList
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using BarCodeAPIService.Models;

namespace BarCodeAPIService.Service
{
    public class PriceListService : IPriceListService
    {
        public Task<ResponseITM1GetPriceList> ResponseITM1GetPriceList()
        {
            var iTM1 = new List<ITM1>();
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('ITM1','','','','','')";
                    login.AD =new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        iTM1.Add(new ITM1
                        {
                            ItemCode=row[0].ToString(),
                            PriceList=Convert.ToInt32(row[1].ToString()),
                            Price=Convert.ToDouble(row[2].ToString()),
                            ListName=row[3].ToString()
                        });
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
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
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

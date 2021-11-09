using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class SaleEmployeeService : ISaleEmployeeService
    {
        public Task<ResponseOSLPGetSalesEmployee> ResponseOSLPGetSalesEmployee()
        {
            var oSLP = new List<OSLP>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OSLP','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        oSLP.Add(new OSLP
                        {
                            SlpCode    = Convert.ToInt32(oRS.Fields.Item(0).Value.ToString()),
                            SlpName     = oRS.Fields.Item(1).Value.ToString(),
                           
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOSLPGetSalesEmployee
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oSLP.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOSLPGetSalesEmployee
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOSLPGetSalesEmployee
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

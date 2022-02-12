using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace BarCodeAPIService.Service
{
    public class BinCodeService : IBinCodeService
    {
        public Task<ResponseOBINGetBinCode> ResponseOBINGetBinCode()
        {          
            var oBIN =new List<OBIN>();
            SAPbobsCOM.Company oCompany;
            try {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    oRS =(SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OBIN','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF) {
                        oBIN.Add(new OBIN
                        {
                            BinCode = oRS.Fields.Item(0).Value.ToString(),
                            WhsCode = oRS.Fields.Item(1).Value.ToString(),
                            WhsName = oRS.Fields.Item(2).Value.ToString()
                        }); 
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOBINGetBinCode
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oBIN.ToList()
                    }); 
                }
                else{
                    return Task.FromResult(new ResponseOBINGetBinCode { 
                        ErrorCode=login.LErrCode,
                        ErrorMessage=login.SErrMsg,
                        Data=null
                    });
                }                              
            }
            catch (Exception ex) {
                return Task.FromResult(new ResponseOBINGetBinCode { 
                    ErrorCode=ex.HResult,
                    ErrorMessage=ex.Message,
                    Data=null
                });
            }
        }
    }
}

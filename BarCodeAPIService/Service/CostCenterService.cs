using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class CostCenterService : ICostCenterService
    {
        public Task<ResponseOPRCGetCostCenter> ResponseOPRCGetCostCenter()
        {
            var oPRC = new List<OPRC>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);                    
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OPRC','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        oPRC.Add(new OPRC
                        {
                            PrcCode = Convert.ToInt32(oRS.Fields.Item(0).Value.ToString()),
                            PrcName = oRS.Fields.Item(1).Value.ToString()
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOPRCGetCostCenter
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oPRC.ToList()
                    });
                }
                else {
                    return Task.FromResult(new ResponseOPRCGetCostCenter { 
                        ErrorCode=login.LErrCode,
                        ErrorMessage=login.SErrMsg,
                        Data=null
                    });
                }                
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOPRCGetCostCenter { 
                    ErrorCode=ex.HResult,
                    ErrorMessage=ex.Message,
                    Data=null
                });
            }
        }
    }
}

using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class GLAccountService : IGLAccountService
    {
        public Task<ResponseOACTGetGLAccount> ResponseOACTGetGLAccount()
        {
            var oACT = new List<OACT>();
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
                        oACT.Add(new OACT
                        {
                            AcctCode= oRS.Fields.Item(0).Value.ToString(),
                            AcctName= oRS.Fields.Item(1).Value.ToString(),                            
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOACTGetGLAccount
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oACT.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOACTGetGLAccount
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOACTGetGLAccount
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

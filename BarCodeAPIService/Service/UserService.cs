using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class UserService : IUserService
    {
        public Task<ResponseOUSRGetUser> ResponseOUSRGetUser()
        {
            var oUSR = new List<OUSR>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    String Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OUSR','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        oUSR.Add(new OUSR
                        {
                            UserCode = oRS.Fields.Item(0).Value.ToString(),
                            UserName = oRS.Fields.Item(1).Value.ToString(),
                            
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOUSRGetUser
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oUSR.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOUSRGetUser
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOUSRGetUser
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

using BarCodeLibrary.Respones.SAP;
using BarCodeAPIService.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class ContactPersonService : IContactPersonService
    {
        public Task<ResponseOCPRGetContactPerson> ResponseOCPRGetContactPerson()
        {
            var oCPR = new List<OCPR>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OCPR','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        oCPR.Add(new OCPR
                        {
                            CardCode = oRS.Fields.Item(0).Value.ToString(),
                            Name = oRS.Fields.Item(1).Value.ToString(),
                            Position = oRS.Fields.Item(2).Value.ToString(),
                            Address = oRS.Fields.Item(3).Value.ToString(),
                            Tel1 = oRS.Fields.Item(4).Value.ToString(),
                            Tel2 = oRS.Fields.Item(5).Value.ToString(),
                            Cellolar = oRS.Fields.Item(6).Value.ToString(),
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOCPRGetContactPerson
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oCPR.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOCPRGetContactPerson
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOCPRGetContactPerson
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

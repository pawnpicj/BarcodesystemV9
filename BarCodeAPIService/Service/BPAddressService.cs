using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class BPAddressService : IBPAddressService
    {
        public Task<ResponseCRD1Address> ResponseCRD1Address()
        {
            var cRD1 = new List<CRD1>();
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
                        cRD1.Add(new CRD1
                        {
                            AdreType = oRS.Fields.Item(0).Value.ToString(),
                            Address = oRS.Fields.Item(1).Value.ToString(),
                            CardCode = oRS.Fields.Item(2).Value.ToString(),
                            Street  = oRS.Fields.Item(3).Value.ToString(),
                            Block     = oRS.Fields.Item(4).Value.ToString(),
                            ZipCode     = oRS.Fields.Item(5).Value.ToString(),
                            City = oRS.Fields.Item(6).Value.ToString(),
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseCRD1Address
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = cRD1.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseCRD1Address
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseCRD1Address
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

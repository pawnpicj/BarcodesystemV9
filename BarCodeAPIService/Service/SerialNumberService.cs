using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class SerialNumberService : ISerialNumberService
    {
        public Task<ResponseOSRIGetSerial> ResponseOSRIGetSerial()
        {
            var oSRI = new List<OSRI>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OSRI','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        oSRI.Add(new OSRI
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            IntrSerial = oRS.Fields.Item(2).Value.ToString(),
                            WhsCode = oRS.Fields.Item(3).Value.ToString(),
                            Quantity = Convert.ToInt32(oRS.Fields.Item(4).Value.ToString())
                        }); ;
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOSRIGetSerial
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oSRI.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOSRIGetSerial
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOSRIGetSerial
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

        }
    }
}

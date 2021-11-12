using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class BatchNumberService : IBatchNumberService
    {
        public Task<ResponseOBTNGetBatch> ResponseOIBTGetBatch()
        {
            var oBIN = new List<OBTN>();
            SAPbobsCOM.Company oCompany;
            try {
                Login login = new();
                if (login.LErrCode == 0) {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OIBT','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        oBIN.Add(new OBTN
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(2).Value.ToString(),
                            ExpDate=oRS.Fields.Item(3).Value.ToString()
                            
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOBTNGetBatch
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oBIN.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOBTNGetBatch { 
                        ErrorCode=login.LErrCode,
                        ErrorMessage=login.SErrMsg,
                        Data=null
                    });
                }
            }

            catch (Exception ex) {
                return Task.FromResult(new ResponseOBTNGetBatch { 
                    ErrorCode=ex.HResult,
                    ErrorMessage=ex.Message,
                    Data=null
                });
            }
        }
    }
}

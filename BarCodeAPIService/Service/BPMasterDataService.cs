using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Request.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class BPMasterDataService : IBPMasterDataService
    {
        private int ErrCode;
        private string ErrMsg;
        
        public Task<ResponseOCRDGetBP> ResponseOCRDGetBP()
        {
            var oCRD = new List<OCRD>();
            
            SAPbobsCOM.Company oCompany;
            try {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string sqlStr = "";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                    oRS.DoQuery(sqlStr);
                    while (!oRS.EoF)
                    {
                        oCRD.Add(new OCRD { 
                            CardCode=oRS.Fields.Item(0).Value.ToString(),
                            CardName=oRS.Fields.Item(1).Value.ToString(),
                            CardFName=oRS.Fields.Item(2).Value.ToString(),
                            CardType=oRS.Fields.Item(3).Value.ToString(),
                            GroupName=oRS.Fields.Item(4).Value.ToString(),
                            Phone=oRS.Fields.Item(5).Value.ToString(),
                            LicTradNum=oRS.Fields.Item(6).ToString()
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOCRDGetBP { 
                        ErrorCode=0,
                        ErrorMessage="",
                        Data=oCRD.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new BarCodeLibrary.Respones.SAP.ResponseOCRDGetBP
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }

            }catch(Exception ex)
            {
                return Task.FromResult(new ResponseOCRDGetBP
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                }); 
            }
        }
    }
}

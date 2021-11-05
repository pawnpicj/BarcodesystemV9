using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public class WarehouseService : IWarehouseService
    {
        public Task<ResponseOWHSGetWarehouse> responseWHSGetWarehouse()
        {
            var oWHS = new List<OWHS>();
            SAPbobsCOM.Company oCompany;
            try {
                Login login = new();
                oCompany = login.Company;
                if (login.LErrCode == 0)
                {
                    SAPbobsCOM.Recordset? oRS;
                    string sqlStr = "";
                    oRS =(SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(sqlStr);
                    while (oRS.EoF)
                    {
                        oWHS.Add(new OWHS
                        {
                            WhsCode=oRS.Fields.Item(0).ToString(),
                            WhsName=oRS.Fields.Item(1).ToString()
                        }
                        );
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseOWHSGetWarehouse
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oWHS.ToList()
                    }); ;
                }
                else
                {
                    return Task.FromResult(new ResponseOWHSGetWarehouse
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage=login.SErrMsg,
                        Data=null
                    }) ;
                }
            } catch (Exception ex) {
                return Task.FromResult(new ResponseOWHSGetWarehouse { 
                    ErrorCode=ex.HResult,
                    ErrorMessage=ex.Message,
                    Data=null
                });
            }
        }
    }
}

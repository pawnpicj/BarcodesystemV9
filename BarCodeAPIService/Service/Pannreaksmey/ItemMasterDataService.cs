using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using BarCodeAPIService.Models;

namespace BarCodeAPIService.Service
{
    public class ItemMasterDataService : IItemMasterDataService
    {
        public Task<ResponseOITMGetItemMaster> ResponseOITMGetItemMaster()
        {
            var oITM = new List<OITM>();
            SAPbobsCOM.Company oCompany;
            DataTable dt = new DataTable();
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset? oRS = null;
                    string Query = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_Smey('OITM','','','','','')";
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        oITM.Add(new OITM
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            ItemFName = oRS.Fields.Item(2).Value.ToString(),
                            ItemGroup = oRS.Fields.Item(3).Value.ToString(),
                            ManBtchNum = oRS.Fields.Item(4).Value.ToString(),
                            ManSerNum = oRS.Fields.Item(5).Value.ToString(),
                            UoM = oRS.Fields.Item(6).Value.ToString(),
                            FDA = oRS.Fields.Item(7).Value.ToString()
                        });
                        oRS.MoveNext();
                    }

                    return Task.FromResult(new ResponseOITMGetItemMaster
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oITM.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOITMGetItemMaster
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }

            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOITMGetItemMaster
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

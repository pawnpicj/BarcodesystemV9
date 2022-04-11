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
            DataTable dt = new DataTable();
            try {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OITM','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oITM.Add(new OITM
                        {
                            ItemCode=row[0].ToString(),
                            ItemName=row[1].ToString(),
                            ItemFName=row[2].ToString(),
                            ItemGroup=row[3].ToString(),
                            ManBtchNum=row[4].ToString(),
                            ManSerNum=row[5].ToString(),
                            UoM=row[6].ToString(),
                            FDA=row[7].ToString()

                        });
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
                    return Task.FromResult(new ResponseOITMGetItemMaster { 
                        ErrorCode=login.lErrCode,
                        ErrorMessage=login.sErrMsg,
                        Data=null
                    });
                }
             
            }
            catch (Exception ex) {
                return Task.FromResult(new ResponseOITMGetItemMaster { 
                    ErrorCode=ex.HResult,
                    ErrorMessage=ex.Message,
                    Data=null
                });
            }
        }
    }
}

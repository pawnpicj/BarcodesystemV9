using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using BarCodeAPIService.Models;
using System.Globalization;
using System.Data.Odbc;
using SAPbobsCOM;

namespace BarCodeAPIService.Service
{
    public class ItemMasterDataService : IItemMasterDataService
    {
        public Task<ResponseOITMGetItemMaster> ResponseOITMGetItemMaster()
        {
            var oITM = new List<OITM>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana);
                if (login.lErrCode == 0)
                {

                    var Query = $"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_Smey('OITM','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oITM.Add(new OITM
                        {
                            ItemCode = row["ItemCode"].ToString(),
                            ItemName = row["ItemName"].ToString(),
                            ItemFName = row["FrgnName"].ToString(),
                            ItemGroup = row["ItmsGrpNam"].ToString(),
                            ManBtchNum = row["ManBtchNum"].ToString(),
                            ManSerNum = row["ManSerNum"].ToString(),
                            UoM = row["UomCode"].ToString(),
                            FDA = row["U_FDA"].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseOITMGetItemMaster
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oITM.ToList()
                    });
                }

                return Task.FromResult(new ResponseOITMGetItemMaster
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
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

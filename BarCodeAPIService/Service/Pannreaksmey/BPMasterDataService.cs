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
    public class BPMasterDataService : IBPMasterDataService
    {

        public Task<ResponseOCRDGetBP> ResponseOCRDGetBP()
        {
            var oCRD = new List<OCRD>();
            DataTable dt = new DataTable();

            try {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OCRD','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oCRD.Add(new OCRD
                        {
                            CardCode = row[0].ToString(),
                            CardName = row[1].ToString(),
                            CardFName = row[2].ToString(),
                            CardType = row[3].ToString(),
                            GroupName = row[4].ToString(),
                            Phone = row[5].ToString(),
                            LicTradNum = row[6].ToString()
                        });
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
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }

            }
            catch (Exception ex)
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Connection;
using BarCodeAPIService.Models;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public class BatchNumberService : IBatchNumberService
    {
        public Task<ResponseOBTNGetBatch> ResponseOIBTGetBatch()
        {
            var oBIN = new List<OBTN>();
            var dt = new DataTable();
            // SAPbobsCOM.Company oCompany;
            try
            {
                // Login login = new();
                var login = new LoginOnlyDatabase();

                if (login.lErrCode == 0)
                {
                    //  oCompany = login.Company;
                    //  SAPbobsCOM.Recordset oRS = null;
                    //  oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);                    
                    // oRS.DoQuery(Query);

                    /*while (!oRS.EoF)
                    {
                        oBIN.Add(new OBTN
                        {
                            ItemCode = oRS.Fields.Item(0).Value.ToString(),
                            ItemName = oRS.Fields.Item(1).Value.ToString(),
                            BatchNumber = oRS.Fields.Item(2).Value.ToString(),
                            ExpDate=oRS.Fields.Item(3).Value.ToString()
                            
                        });
                        oRS.MoveNext();
                    }*/
                    var Query = "CALL \"" + ConnectionString.CompanyDB +
                                "\"._USP_CALLTRANS_Smey ('OIBT','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        oBIN.Add(new OBTN
                        {
                            ItemCode = row[0].ToString(),
                            ItemName = row[1].ToString(),
                            BatchNumber = row[2].ToString(),
                            ExpDate = row[3].ToString()
                        });
                    return Task.FromResult(new ResponseOBTNGetBatch
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oBIN.ToList()
                    });
                }

                return Task.FromResult(new ResponseOBTNGetBatch
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOBTNGetBatch
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}
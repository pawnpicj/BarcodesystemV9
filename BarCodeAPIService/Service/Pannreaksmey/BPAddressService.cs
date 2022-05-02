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
    public class BPAddressService : IBPAddressService
    {
        public Task<ResponseCRD1Address> responseCRD1Address()
        {
            var cRD1 = new List<CRD1>();
            var dt = new DataTable();
            try
            {
                var login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    var Query = "CALL \"" + ConnectionString.CompanyDB +
                                "\"._USP_CALLTRANS_Smey ('CRD1','','','','','')";
                    login.AD = new OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                        cRD1.Add(new CRD1
                        {
                            AdreType = row[0].ToString(),
                            Address = row[1].ToString(),
                            CardCode = row[2].ToString(),
                            Street = row[3].ToString(),
                            Block = row[4].ToString(),
                            ZipCode = row[5].ToString(),
                            City = row[6].ToString()
                        });
                    return Task.FromResult(new ResponseCRD1Address
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = cRD1.ToList()
                    });
                }

                return Task.FromResult(new ResponseCRD1Address
                {
                    ErrorCode = login.lErrCode,
                    ErrorMessage = login.sErrMsg,
                    Data = null
                });
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
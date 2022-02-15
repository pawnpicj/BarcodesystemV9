using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service.Pannreaksmey
{
    public class SeriesIMService : ISeriesIMService
    {
        public Task<ResponseNNM1_IM> responseNNM1_IM()
        {
            var oSIM = new List<NNM1IM>();
            SAPbobsCOM.Company oCompany;
            try
            {
                Login login = new();
                if (login.LErrCode == 0)
                {
                    oCompany = login.Company;
                    SAPbobsCOM.Recordset oRS = null;
                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_BANK ('NNM1IM','','','','','')";
                    oRS.DoQuery(Query);
                    while (!oRS.EoF)
                    {
                        oSIM.Add(new NNM1IM
                        {
                            ObjectCode = oRS.Fields.Item(0).Value.ToString(),
                            Series = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
                            SeriesName = oRS.Fields.Item(2).Value.ToString(),
                            Indicator = oRS.Fields.Item(3).Value.ToString(),
                            BeginStr = oRS.Fields.Item(4).Value.ToString()
                        });
                        oRS.MoveNext();
                    }
                    return Task.FromResult(new ResponseNNM1_IM
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oSIM.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseNNM1_IM
                    {
                        ErrorCode = login.LErrCode,
                        ErrorMessage = login.SErrMsg,
                        Data = null
                    });
                }
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ResponseNNM1_IM
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        
    }
    
    }

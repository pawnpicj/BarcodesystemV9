using BarCodeLibrary.Respones.SAP;
using BarCodeAPIService.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using BarCodeAPIService.Models;

namespace BarCodeAPIService.Service
{
    public class DocumentSeriesService:IDocumentSeriesService
    {
        //Task<ResponseNNM1GetDocumentSeries> ResponseNNM1GetDocumentSeries() {
        //    var nMM1 = new List<NNM1>();
        //    SAPbobsCOM.Company oCompany;
        //    try
        //    {
        //        Login login = new();
        //        if (login.LErrCode == 0)
        //        {
        //            oCompany = login.Company;
        //            SAPbobsCOM.Recordset oRS = null;
        //            oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
        //            string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('NNM1','','','','','')";
        //            oRS.DoQuery(Query);
        //            while (!oRS.EoF)
        //            {
        //                nMM1.Add(new NNM1
        //                {
        //                    ObjectCode = oRS.Fields.Item(0).Value.ToString(),
        //                    Series = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
        //                    SeriesName = oRS.Fields.Item(0).Value.ToString(),
        //                    InitialNum = Convert.ToInt32(oRS.Fields.Item(1).Value.ToString()),
        //                    NextNumber = Convert.ToInt32(oRS.Fields.Item(0).Value.ToString()),
        //                    Indicator = oRS.Fields.Item(1).Value.ToString()

        //                });
        //                oRS.MoveNext();
        //            }
        //            return Task.FromResult(new ResponseNNM1GetDocumentSeries
        //            {
        //                ErrorCode = 0,
        //                ErrorMessage = "",
        //                Data = nMM1.ToList()
        //            });
        //        }
        //        else
        //        {
        //            return Task.FromResult(new ResponseNNM1GetDocumentSeries
        //            {
        //                ErrorCode = login.LErrCode,
        //                ErrorMessage = login.SErrMsg,
        //                Data = null
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.FromResult(new ResponseNNM1GetDocumentSeries
        //        {
        //            ErrorCode = ex.HResult,
        //            ErrorMessage = ex.Message,
        //            Data = null
        //        });
        //    }
        //}

        Task<ResponseNNM1GetDocumentSeries> IDocumentSeriesService.responseNNM1GetDocumentSeries()
        {
            var nMM1 = new List<NNM1>();
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    string Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('NNM1','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        nMM1.Add(new NNM1
                        {
                            ObjectCode=row[0].ToString(),
                            Series=Convert.ToInt32(row[1].ToString()),
                            SeriesName=row[2].ToString(),
                            InitialNum=Convert.ToInt32(row[3].ToString()),
                            NextNumber=Convert.ToInt32(row[4].ToString()),
                            Indicator=row[5].ToString()
                        });
                    }                    
                    return Task.FromResult(new ResponseNNM1GetDocumentSeries
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = nMM1.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseNNM1GetDocumentSeries
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseNNM1GetDocumentSeries
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}

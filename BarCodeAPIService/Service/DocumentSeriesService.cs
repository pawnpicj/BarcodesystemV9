//using BarCodeLibrary.Respones.SAP;
//using BarCodeAPIService.Connection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BarCodeAPIService.Service
//{
//    public class DocumentSeriesService:IDocumentSeriesService
//    {
//        Task<ResponseNNM1GetDocumentSeries> ResponseNNM1GetDocumentSeries() {
//            var nMM1 = new List<NNM1>();
//            SAPbobsCOM.Company oCompany;
//            try
//            {
//                Login login = new();
//                if (login.LErrCode == 0)
//                {
//                    oCompany = login.Company;
//                    SAPbobsCOM.Recordset oRS = null;
//                    oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
//                    string query = "";
//                    oRS.DoQuery(query);
//                    while (!oRS.EoF)
//                    {
//                        nMM1.Add(new NNM1
//                        {
//                            UserCode = oRS.Fields.Item(0).Value.ToString(),
//                            UserName = oRS.Fields.Item(1).Value.ToString(),

//                        });
//                        oRS.MoveNext();
//                    }
//                    return Task.FromResult(new ResponseNNM1GetDocumentSeries
//                    {
//                        ErrorCode = 0,
//                        ErrorMessage = "",
//                        Data = nMM1.ToList()
//                    });
//                }
//                else
//                {
//                    return Task.FromResult(new ResponseNNM1GetDocumentSeries
//                    {
//                        ErrorCode = login.LErrCode,
//                        ErrorMessage = login.SErrMsg,
//                        Data = null
//                    });
//                }
//            }
//            catch (Exception ex)
//            {
//                return Task.FromResult(new ResponseNNM1GetDocumentSeries
//                {
//                    ErrorCode = ex.HResult,
//                    ErrorMessage = ex.Message,
//                    Data = null
//                });
//            }
//        }
//    }
//}

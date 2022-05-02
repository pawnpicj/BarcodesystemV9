using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP.Bank;

namespace BarCodeAPIService.Service.Bank
{
    public interface ISeriesCVService
    {
        Task<ResponseNNM1_CV> responseNNM1_CV();
        Task<ResponseGetSeriesCode> responseGetSeriesCode(string yymm, string typeSeries);
    }
}
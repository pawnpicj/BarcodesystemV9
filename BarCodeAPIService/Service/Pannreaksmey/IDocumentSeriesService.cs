using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IDocumentSeriesService
    {
        Task<ResponseNNM1GetDocumentSeries> responseNNM1GetDocumentSeries();
    }
}
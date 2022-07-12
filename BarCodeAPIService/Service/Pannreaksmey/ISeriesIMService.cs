using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service.Pannreaksmey
{
    public interface ISeriesIMService
    {
        Task<ResponseNNM1_IM> responseNNM1_IM();
    }
}
using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IBPAddressService
    {
        Task<ResponseCRD1Address> responseCRD1Address();
    }
}
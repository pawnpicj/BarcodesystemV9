using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IInventoryTransferRequestService
    {
        Task<ResponseGetOWTQ> responseGetOWTQ();
        Task<ResponseGetWTQLine> responseGetWTQLine(int DocEntry);
    }
}
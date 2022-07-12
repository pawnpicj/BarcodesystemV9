using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IInventoryCountingService
    {
        Task<ResponseInventoryCounting> responseInventoryCounting(SendInventoryCounting SendInventoryCounting);
    }
}
using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IInventoryTransferService
    {
        Task<ResponseInventoryTransfer> responseInventoryTransfer(SendInventoryTransfer SendInventoryTransfer);
        Task<ResponseInventoryTransfer> responseInventoryTransferCV(SendInventoryTransfer SendInventoryTransfer);
        Task<ResponseInventoryTransfer> responseInventoryTransferIM(SendInventoryTransfer SendInventoryTransfer);
    }
}
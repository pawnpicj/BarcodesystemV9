using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IInventoryTransferIMService
    {

        Task<ResponseGetOWTR> responseGetOWTR();
        Task<ResponseGetWTRLine> responseGetWTRLine(int DocEntry);
        Task<ResponseGetIMHeadLine> responseGetIMByCus(string cusCode);
        Task<ResponseIMReport> responseIMReport(string fromDate, string toDate, string customer, string saleEmp);
    }
}
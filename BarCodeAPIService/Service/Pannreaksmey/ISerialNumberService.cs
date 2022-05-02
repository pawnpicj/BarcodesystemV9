using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface ISerialNumberService
    {
        Task<ResponseOSRIGetSerial> ResponseOSRIGetSerial();
    }
}
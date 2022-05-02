using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public interface IContactPersonService
    {
        Task<ResponseOCPRGetContactPerson> ResponseOCPRGetContactPerson();
    }
}
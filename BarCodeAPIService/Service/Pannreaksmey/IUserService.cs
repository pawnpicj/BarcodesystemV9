using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP.Pannreaksmey;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;

namespace BarCodeAPIService.Service
{
    public interface IUserService
    {
        Task<ResponseOUSRGetUser> ResponseOUSRGetUser();
        Task<ResponsePostUser> ResponsePostUserAsync(SendUser send);
        Task<ResponseGetUser> RespponseGetuser();
    }
}
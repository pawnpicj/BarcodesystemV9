using System.Threading.Tasks;
using Barcodesystem.Request.Token;
using Barcodesystem.Respones.Token;

namespace BarCodeAPIService.Service
{
    public interface ITokenService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<RefreshResponse> RefrestAsync(RefreshRequest refreshRequest);
    }
}
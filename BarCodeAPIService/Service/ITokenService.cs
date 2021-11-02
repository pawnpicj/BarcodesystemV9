using Barcodesystem.Request.Token;
using Barcodesystem.Respones.Token;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public interface ITokenService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<RefreshResponse> RefrestAsync(RefreshRequest refreshRequest);
    }
}

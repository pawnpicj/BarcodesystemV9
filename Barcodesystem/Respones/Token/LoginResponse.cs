namespace Barcodesystem.Respones.Token
{
    public class LoginResponse
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
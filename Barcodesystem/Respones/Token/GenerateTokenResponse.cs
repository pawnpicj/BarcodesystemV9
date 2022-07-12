namespace Barcodesystem.Respones.Token
{
    public class GenerateTokenResponse
    {
        public string Token { get; set; } = null!;
        public string ValidFrom { get; set; } = null!;
        public string ValidTo { get; set; } = null!;
    }
}
using System.ComponentModel.DataAnnotations;

namespace Barcodesystem.Request.Token
{
    public class RefreshRequest
    {
        [Required] public string CardCode { get; set; } = null!;

        [Required] public string Token { get; set; } = null!;
    }
}
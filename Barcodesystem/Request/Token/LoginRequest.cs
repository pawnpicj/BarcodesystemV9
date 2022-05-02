using System.ComponentModel.DataAnnotations;

namespace Barcodesystem.Request.Token
{
    public class LoginRequest
    {
        [Required] public string CardCode { get; set; } = null!;

        [Required] public string Password { get; set; } = null!;
    }
}
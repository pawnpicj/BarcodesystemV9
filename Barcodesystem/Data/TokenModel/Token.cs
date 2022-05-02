using System;

namespace Barcodesystem.Model.TokenModel
{
    public class Token
    {
        public string CardCode { get; set; } = null!;
        public string TokenString { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
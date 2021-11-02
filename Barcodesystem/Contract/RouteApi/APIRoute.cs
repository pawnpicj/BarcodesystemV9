namespace Barcodesystem.Contract.RouteApi
{
    public static class APIRoute
    {
        public const string Root = "api/[controller]";
        public static class Token
        {
            public const string Login = "login";
            public const string RefreshToken = "refresh";
        }
    }
}

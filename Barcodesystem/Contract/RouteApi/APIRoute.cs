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
        public static class GoodReceiptPO
        {
            public const string Controller = "api/GoodsReceiptPO/";
            public const string GetCustomer = "GetCustomer";
            public const string GetPO = "GetPO/";
            public const string SendGoodReceiptPO = "SendGoodReceiptPO";
        }
    }
}

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
            public const string GetGoodReturn = "GoodReturn/";
            public const string SendGoodReceiptPO = "SendGoodReceiptPO";
            public const string GetSeries = "Series/";
            public const string GetSaleEmployee = "SaleEmployee";
            public const string GetCurrency = "GetCurrency/";
            public const string GetGenerate_Serial_Batch = "GetSerialOrBatch";
            public const string GetItemCode = "GetItem";
            public const string GetTaxCode = "GetTaxCode";
            public const string GetWarehouse = "GetWarehouse";
            public const string GetUnitOfMeasure = "GetUOM/";
            public const string GetBatchGenerator = "GetBatchGen";
        }
        public static class GoodReturn
        {
            public const string Controller = "api/GoodsReturn/";
            public const string GetGoodReceiptPO = "GetGoodsReceiptPO/";
            public const string GetGoodReceiptPOByDocNum = "GetGoodsReceiptPOByDocNum/";
            public const string SendGoodsReturn = "SendGoodsReturn";
        }
    }
}
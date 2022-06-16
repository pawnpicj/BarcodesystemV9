namespace BarCodeLibrary.Contract.RouteProcedure
{
    public static class ProcedureRoute
    {
        public const string _USP_CALLTRANS_TENGKIMLEANG = "_USP_CALLTRANS_TENGKIMLEANG";
        public const string _USP_GENERATE_SERIAL_SqlHana = "_USP_GENERATE_Serial";
        public const string _USP_GENERATE_Batch_SqlHana = "_USP_GENERATE_Batch";

        public static class Type
        {
            public const string CustomerGet = "OCRD";
            public const string GetPO = "OPOR";
            public const string GetPOLine = "POR1";
            public const string GetSeries = "NNM1";
            public const string GetSaleEmployee = "OSLP";
            public const string GetCurrency = "OCRN";
            public const string GetItemCode = "OITM";
            public const string GetVatCode = "OVTG";
            public const string GetWarehouse = "OWHS";
            public const string GetUom = "OUOM";
        }
    }
}
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
            public const string GetSO = "ORDR";
            public const string GetPOLine = "POR1";
            public const string GetSOLine = "RDR1";
            public const string GetSeries = "NNM1";
            public const string GetSaleEmployee = "OSLP";
            public const string GetCurrency = "OCRN";
            public const string GetItemCode = "OITM";
            public const string GetVatCode = "OVTG";
            public const string GetWarehouse = "OWHS";
            public const string GetUom = "OUOM";
            public const string GetGoodRecieptPO = "OPDN";
            public const string GetGoodReturn = "ORPD";
            public const string GetBarCode = "OBCD";
            public const string GetBatch = "Batch";
            public const string GetSerial = "Serial";
            public const string GetDelivery = "ODLN";
            public const string GetDeliveryLine = "DLN1";
            public const string GetSaleOrder = "GETSALEORDER";
        }
    }
}
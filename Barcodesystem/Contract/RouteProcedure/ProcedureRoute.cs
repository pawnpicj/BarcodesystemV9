using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Contract.RouteProcedure
{
    public static class ProcedureRoute
    {
        public const string _USP_CALLTRANS_TENGKIMLEANG = "_USP_CALLTRANS_TENGKIMLEANG";
        public static class Type
        {
            public const string CustomerGet = "OCRD";
            public const string GetPO = "OPOR";
            public const string GetPOLine = "POR1";
            public const string GetSeries = "NNM1";
            public const string GetSaleEmployee = "OSLP";
            public const string GetCurrency = "OCRN";
        }
    }
}

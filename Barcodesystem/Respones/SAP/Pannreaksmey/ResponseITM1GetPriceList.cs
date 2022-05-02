using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseITM1GetPriceList
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<ITM1> Data { get; set; }
    }

    public class ITM1
    {
        public string ItemCode { get; set; }
        public int PriceList { get; set; }
        public double Price { get; set; }
        public string ListName { get; set; }
    }
}
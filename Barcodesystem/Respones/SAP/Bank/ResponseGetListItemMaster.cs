using System;
using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class ResponseGetListItemMaster
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<lMaster> Data { get; set; }
    }

    public class lMaster
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UOM { get; set; }        
        public double Quantity { get; set; }
        public string BatchYN { get; set; }
        public string SerialYN { get; set; }
        public string Status { get; set; }        
    }
}
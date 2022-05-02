﻿using System;
using System.Collections.Generic;

namespace BarCodeLibrary.Request.SAP
{
    public class SendDelivery
    {
        public string CardCode { get; set; }
        public string NumAtCard { get; set; }
        public DateTime DocDate { get; set; }

        public int ContactPersion { get; set; }

        //public string DocType { get; set; }
        public List<SendDeliveryLine> Lines { get; set; }
    }

    public class SendDeliveryLine
    {
        public string ItemCode { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public string TaxCode { get; set; }
        public string WarehouseCode { get; set; }
        public string UomCode { get; set; }
    }
}
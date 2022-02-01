﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseGetWTQLine
    {
        public int ErrorCode { get; set; }
        public string? ErrorMsg { get; set; }
        public List<WTQLine> Data { get; set; }
    }
    public class WTQLine
    {
        public int DocEntry { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public double Quantity { get; set; }
        public string FromWhsCod { get; set; }
        public string WhsCode { get; set; }
        public string UomCode { get; set; }
        public string unitMsr { get; set; }
        public double U_unitprice { get; set; }
    }
}

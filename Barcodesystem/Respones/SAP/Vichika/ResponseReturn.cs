﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Vichika
{
    public class ResponseReturn
    {
        public int ErrorCode { get; set; }
        public string? ErrorMsg { get; set; }
        public string? DocEntry { get; set; }
    }
}

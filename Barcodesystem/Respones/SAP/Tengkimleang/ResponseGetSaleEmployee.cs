﻿using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Tengkimleang
{
    public class ResponseGetSaleEmployee
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetSaleEmployee> Data { get; set; }
    }

    public class GetSaleEmployee
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
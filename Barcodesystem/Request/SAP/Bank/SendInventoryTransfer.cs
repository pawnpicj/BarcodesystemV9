﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP
{
    public class SendInventoryTransfer
    {
        //Head
        public int Series { get; set; }
        //public int DocNum { get; set; }
        //public string DocEntry { get; set; }
        public string CardCode { get; set; }

        public DateTime DocDate { get; set; }
        public DateTime TaxDate { get; set; }

        public string FromWhsCode { get; set; }
        public string ToWhsCode { get; set; }
        public string ToBinCode { get; set; }
        public int ToBinEntry { get; set; }

        public string SalesEmployeeCode { get; set; }

        public string Comments { get; set; }
        public string JournalRemark { get; set; }

        public List<SendInventoryTransferLine> Line { get; set; }
        
    }

    public class SendInventoryTransferLine
    {
        //Line
        public string ItemCode { get; set; }
        //public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string FromWhsCode { get; set; }
        public string ToWhsCode { get; set; }

        public string BatchNo { get; set; }
        public string SerialNo { get; set; }

        public int fromBinEntry { get; set; }
        public int toBinEntry { get; set; }

        public string U_TranferNo { get; set; }

        public List<SendInventoryTransferBatch> Batch { get; set; }
        public List<SendInventoryTransferSerial> Serial { get; set; }
        public List<InventoryTransferBinLocation> FromBinLocations { get; set; }
        public List<InventoryTransferBinLocation> ToBinLocations { get; set; }
    }

    public class SendInventoryTransferBatch
    {
        public string BatchCode { get; set; }
        public int Quantity { get; set; }
    }

    public class SendInventoryTransferSerial
    {
        public string SerialCode { get; set; }
        public int Quantity { get; set; }
    }

    public class InventoryTransferBinLocation
    {
        public string BinEntry { get; set; }
        public long Quantity { get; set; }
        //public long SerialBatchNubmberBaseLine { get; set; }
    }
}

using System;
using System.Collections.Generic;

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

        public string ShipToCode { get; set; }
        public string Address { get; set; }
        
        public string FromWhsCode { get; set; }
        public string ToWhsCode { get; set; }
        public string ToBinCode { get; set; }
        public int ToBinEntry { get; set; }

        public int SalesEmployeeCode { get; set; }

        public string Comments { get; set; }
        public string JournalRemark { get; set; }

        public string U_loannum { get; set; }
        public string U_WebID { get; set; }
        public string U_pname { get; set; }

        public List<SendInventoryTransferLine> Line { get; set; }
        //public List<SendInventoryTransferBatch> Batch { get; set; }
        //public List<SendInventoryTransferSerial> Serial { get; set; }
    }

    public class SendInventoryTransferLine
    {
        //Line
        public string ItemCode { get; set; }

        //public string ItemName { get; set; }
        public double Quantity { get; set; }
        public double QtyInSap { get; set; }
        public double InputQty { get; set; }
        public double TotalQty { get; set; }
        public double Price { get; set; }
        public int UomEntry { get; set; }
        public string UomCode { get; set; }
        public string FromWhsCode { get; set; }
        public string ToWhsCode { get; set; }

        public string BatchNo { get; set; }
        public string SerialNo { get; set; }

        public int fromBinEntry { get; set; }
        public int toBinEntry { get; set; }

        public string U_TranferNo { get; set; }
        public string U_Patient { get; set; }
        public string U_BalanceQty { get; set; }

        public string ProductType { get; set; }

        public int BaseEntry { get; set; }
        public int BaseType { get; set; }
        public int BaseLine { get; set; }

        public string mStatus { get; set; }


        public List<ItfBatchNumLine> BatchLine { get; set; }
        public List<ItfSerialNumLine> SerialLine { get; set; }
        public List<InventoryTransferBinLocation> FromBinLocations { get; set; }
        public List<InventoryTransferBinLocation> ToBinLocations { get; set; }
    }

    public class ItfBatchNumLine
    {
        public string ItemCode { get; set; }
        public string BatchNumber { get; set; }
        public int fromBinEntryX { get; set; }
        public int Quantity { get; set; }
        public string ProductType { get; set; }
    }

    public class ItfSerialNumLine
    {
        public string ItemCode { get; set; }
        public string SerialNumber { get; set; }
        public int fromBinEntryX { get; set; }
        public int Quantity { get; set; }
        public string ProductType { get; set; }
    }

    public class InventoryTransferBinLocation
    {
        public string BinEntry { get; set; }
        public long Quantity { get; set; }
    }
}
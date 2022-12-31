using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Bank
{
    public class ResponsePrintLableINF
    {
        public List<Data> Data { get; set; }
    }

    public static class PrintLableINFStatic
    {
        public static List<Data> Data { get; set; }
    }

    public class Data
    {
        public string DocNum { get; set; }
        public int DocEntry { get; set; }
        public string BPCode { get; set; }
        public string BPName { get; set; }
        public string PostingDate { get; set; }
        public List<DataLine> Line { get; set; }
    }

    public class DataLine
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public string batchNumber { get; set; }
        public string serialNumber { get; set; }
        public double qtyByBatch { get; set; }
        public double qtyBySerial { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Vichika
{
    public class ResponseODLNGetDelivery
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetDelivery> Data { get; set; }
    }

    public class GetDelivery
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public int CntctCode { get; set; }
        public string NumAtCard { get; set; }
        public int DocNum { get; set; }
        public int DocEntry { get; set; }
        public string DocStatus { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public DateTime TaxDate { get; set; }
        public double DocTotal { get; set; }
        public double DiscPrcnt { get; set; }
        public string Remark { get; set; }
        public string SlpCode { get; set; }
        public string SlpName { get; set; }
        public string BPCurrency { get; set; }
        public List<GetDeliveryLine> Line { get; set; }
    }

    public class GetDeliveryLine
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public double Quatity { get; set; }
        public double Price { get; set; }
        public double DiscPrcnt { get; set; }
        public string VatGroup { get; set; }
        public double LineTotal { get; set; }
        public string WhsCode { get; set; }
        public int LineNum { get; set; }
        public int BaseEntry { get; set; }
        public string ManageItem { get; set; }
    }
}

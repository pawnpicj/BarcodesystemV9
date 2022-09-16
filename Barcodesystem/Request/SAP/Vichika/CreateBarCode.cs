using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Request.SAP.Vichika
{
    public class CreateBarCodeSaleOrder
    {
        public List<BarCodeSaleOrder> Data { get; set; }
    }

    public static class BarCodeSaleOrderStatic
    {
        public static List<BarCodeSaleOrder> Data { get; set; }
    }

    public class BarCodeSaleOrder
    {
        public string DocNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public string DeliveryDate { get; set; }
    }
}

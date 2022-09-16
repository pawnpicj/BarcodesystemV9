using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.Respones.SAP.Vichika
{
    public class ResponseGetSaleOrder
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetSaleOrder> Data { get; set; }
    }

    public class GetSaleOrder
    {
        public string DocNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public string DeliveryDate { get; set; }
    }
}

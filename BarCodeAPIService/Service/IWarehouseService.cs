using System;
using BarCodeLibrary.Respones.SAP;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public interface IWarehouseService
    {
        Task<ResponseOWHSGetWarehouse> responseWHSGetWarehouse();
    }
}

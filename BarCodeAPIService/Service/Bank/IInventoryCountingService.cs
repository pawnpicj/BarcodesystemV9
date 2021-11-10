using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public interface IInventoryCountingService
    {
        Task<ResponseInventoryCounting> responseInventoryCounting(SendInventoryCounting SendInventoryCounting);
    }
}

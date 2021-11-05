using System;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Request.SAP;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service
{
    public interface IBPMasterDataService
    {
        Task<ResponseOCRDGetBP> ResponseOCRDGetBP();
    }
}

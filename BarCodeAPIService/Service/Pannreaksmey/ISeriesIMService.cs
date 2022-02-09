using BarCodeLibrary.Respones.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Service.Pannreaksmey
{
    public interface ISeriesIMService
    {
        Task<ResponseNNM1_IM> responseNNM1_IM();
    }
}

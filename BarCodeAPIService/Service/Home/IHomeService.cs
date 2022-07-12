using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP.Home;

namespace BarCodeAPIService.Service.Home
{
    public interface IHomeService
    {
        Task<LoginAsync> GetLoginAsync(string user,string password);
    }
}

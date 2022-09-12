using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.Respones.SAP.Home;
using BarCodeAPIService.Connection;

namespace BarCodeAPIService.Service.Home
{
    public class HomeService:IHomeService
    {
        public Task<LoginAsync> GetLoginAsync(string user,string password)
        {
            var clsCRUD = new ClsCRUD();
            var dt = clsCRUD.GetDataWeb("SELECT * FROM \""+ConnectionString.BarcodeDb+"\".TBUSER WHERE USERCODE='" + user+ "' And PASSWORD='"+password+ "' And ACTIVE='Y'", "WebDb");
            if (dt != null)
            {
                return Task.FromResult(new LoginAsync
                {
                    ID=1,
                    Name=user.ToString(),
                    Password=dt.Rows[0][7].ToString(),
                    Permission=dt.Rows[0][8].ToString()
                });
            }
            else
            {
                return Task.FromResult(new LoginAsync { 
                    ID=0,Name="",Password=""
                });
            }

        }
    }
}

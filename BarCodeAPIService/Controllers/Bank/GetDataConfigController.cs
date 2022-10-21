using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using BarCodeLibrary.Respones.SAP.Bank;
using Microsoft.Extensions.Options;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Controllers.Bank
{
    [Produces("application/json")]
    [Route("api/GetDataConfig")]
    public class GetDataConfigController : Controller
    {
        private IConfiguration configuration;
        public GetDataConfigController(IConfiguration iConfig)
        {
            this.configuration = iConfig;
        }

        private readonly IOptions<ResponseGetDataConfig> dataConfig;

        [HttpGet]
        public IActionResult GetDataConfig()
        {

            var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            var list = new List<string>();
            list.Add(configuration["CompanyDB"]);
            list.Add(configuration["UserNameSAP"]);
            return Ok(list);

        }


    }
}

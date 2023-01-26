using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using BarCodeLibrary.Respones.SAP.Bank;
//using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace BarCodeClientService.Controllers
{
    public class SettingController : Controller
    {
        private IHostingEnvironment Environment;

        public SettingController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostSetting(SetupTypeModel setupTypeModel)
        {
            string path = $"{Environment.WebRootPath}\\js\\setup-inc.json";
            string json = JsonConvert.SerializeObject(setupTypeModel);

            using (var tc = new StreamWriter(path, false))
            {
                tc.Write(String.Empty);
                tc.Close();
            }

            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(json.ToString());
                tw.Close();
            }
            return Ok(json);
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP.Pannreaksmey;
using BarCodeLibrary.Respones.SAP.Bank;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class UserController : ControllerBase
    {
        private readonly IUserService user;

        public UserController(IUserService user)
        {
            this.user = user;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            var a = await user.ResponseOUSRGetUser();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpPost("PostUser")]
        public async Task<IActionResult> PostUserAsync(SendUser send)
        {
            return Ok(await user.ResponsePostUserAsync(send));
        }

        [HttpGet("GetUserLogin")]
        public async Task<IActionResult> GetUserLoginAsync()
        {
            var a = await user.ResponseGetuser();
            return Ok(a);
        }

        [HttpGet("GetConfig")]
        public async Task<IActionResult> GetConfigAsync()
        {
            var a = await user.ResponseGetDataConfig();
            return Ok(a);
        }

        [HttpGet("GetUserConfig")]
        public async Task<IActionResult> GetUserConfigAsync()
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var CompanyDB = MyConfig.GetValue<string>("CompanyDB");
            var UserNameSAPX = MyConfig.GetValue<string>("UserNameSAPX");
            var PasswordX = MyConfig.GetValue<string>("PasswordX");

            List<ListData> ObjData = new List<ListData>()
            {
                new ListData {
                    CompanyDB=CompanyDB,
                    UserNameSAPX=UserNameSAPX,
                    PasswordX=PasswordX
                }
            };
            
            var a = await user.ResponseGetDataConfig();
            return Ok(ObjData);
        }

        [HttpPost("PostUserConfig")]
        public async Task<IActionResult> PostUserConfigAsync(SendUser send)
        {
            //var filePath = @"C:\inetpub\backup\Barcodesystem\BarCodeAPIService\appsettings.json";
            var filePath = Path.Combine(AppContext.BaseDirectory, "appSettings.json");
            string json = System.IO.File.ReadAllText(filePath);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            var sectionPathUserX = "UserNameSAP";
            var sectionPathPasswX = "Password";

            if (!string.IsNullOrEmpty(sectionPathUserX))
            {
                jsonObj[sectionPathUserX] = send.UserNameSAP;
                jsonObj[sectionPathPasswX] = send.Password;
            }

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(filePath, output);
            //return Ok("appSettings Update.");
            //Success
            return Ok(send);
        }

        [HttpPost("UpdatePasswordUser")]
        public async Task<IActionResult> PostUpdatePasswordAsync(SendUser send)
        {

            return Ok(await user.ResponseUpdatePasswordAsync(send));
        }
    }
}
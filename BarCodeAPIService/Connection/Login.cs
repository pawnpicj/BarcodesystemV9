using System;
using System.IO;
using Newtonsoft.Json;
using SAPbobsCOM;

//using Microsoft.IdentityModel.Protocols;

namespace BarCodeAPIService.Connection
{
    public class Login
    {
        // Protected Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        private int _lErrCode;
        private string _sErrMsg = null!;

        public Login()
        {
            LogIn1();
        }

        public string SErrMsg => _sErrMsg;

        public int LErrCode => _lErrCode;

        public Company Company { get; private set; } = null!;        

        private void LogIn1()
        {
            Company oCompany;
            //string Server = "";
            //string DbServerType = "";
            //string LicenseServer = "";
            //string DbUserName = "";
            //string DbPassword = "";
            //string CompanyDB = "";
            //string UserName = "";
            //string Password = "";
            //string SLDServer = "";
            try
            {
                // log4net.Config.XmlConfigurator.Configure()
                oCompany = new Company();
                // Set connection properties
                switch (ConnectionString.DbServerType)
                {
                    case "dst_MSSQL":
                    {
                        oCompany.DbServerType = BoDataServerTypes.dst_MSSQL;
                        break;
                    }
                    case "dst_DB_2":
                    {
                        oCompany.DbServerType = BoDataServerTypes.dst_DB_2;
                        break;
                    }

                    case "dst_SYBASE":
                    {
                        oCompany.DbServerType = BoDataServerTypes.dst_SYBASE;
                        break;
                    }

                    case "dst_MSSQL2005":
                    {
                        oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2005;
                        break;
                    }

                    case "dst_MAXDB":
                    {
                        oCompany.DbServerType = BoDataServerTypes.dst_MAXDB;
                        break;
                    }

                    case "dst_MSSQL2008":
                    {
                        oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2008;
                        break;
                    }

                    case "dst_MSSQL2012":
                    {
                        oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2012;
                        break;
                    }

                    case "dst_MSSQL2014":
                    {
                        oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2014;
                        break;
                    }

                    case "HANADB":
                    {
                        oCompany.DbServerType = BoDataServerTypes.dst_HANADB;
                        break;
                    }
                }



                //string tmpstr;
                oCompany.Server = ConnectionString.Server;
                //tmpstr = oCompany.Server;
                oCompany.LicenseServer = ConnectionString.LicenseServer;
                //tmpstr = oCompany.LicenseServer;
                oCompany.language = BoSuppLangs.ln_English; // change to your language
                oCompany.UseTrusted = false;
                oCompany.DbUserName = ConnectionString.DbUserName;
                //tmpstr = oCompany.DbUserName;
                oCompany.DbPassword = ConnectionString.DbPassword;
                oCompany.CompanyDB = ConnectionString.CompanyDB;

                //Read User.js
                //var filePath3 = Path.Combine(AppContext.BaseDirectory, "wwwroot\\js\\UserClient.json");
                var filePath3 = Path.Combine(AppContext.BaseDirectory, "appSettings.json");
                string json3 = System.IO.File.ReadAllText(filePath3);
                dynamic jsonObj3 = JsonConvert.DeserializeObject(json3);
                string username = jsonObj3["UserNameSAP"];
                string passw0rd = jsonObj3["Password"];

                if (username != "" && passw0rd != "")
                {
                    oCompany.UserName = username;
                    oCompany.Password = passw0rd;
                }
                else
                {
                    oCompany.UserName = ConnectionString.UserName;
                    oCompany.Password = ConnectionString.Password;
                }

                
                //oCompany.SLDServer = ConnectionString.SLDServer;

                if (oCompany.Connect() != 0)
                {
                    oCompany.GetLastError(out _lErrCode, out _sErrMsg);
                    Company = null!;
                }
                else
                {
                    _lErrCode = 0;
                    _sErrMsg = "";
                    Company = oCompany;
                }
            }
            catch (Exception ex)
            {
                _lErrCode = ex.GetHashCode();
                _sErrMsg = ex.Message;
                Company = null!;
            }
        }
    }
}
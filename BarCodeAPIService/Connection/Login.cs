using System;
//using Microsoft.IdentityModel.Protocols;

namespace BarCodeAPIService.Connection
{
    public class Login
    {
        // Protected Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        private SAPbobsCOM.Company _Company = null!;
        private int _lErrCode = 0!;
        private string _sErrMsg = null!;

        public string SErrMsg
        {
            get
            {
                return _sErrMsg;
            }
        }
        public int LErrCode
        {
            get
            {
                return _lErrCode;
            }
        }

        public SAPbobsCOM.Company Company
        {
            get
            {
                return _Company;
            }
        }

        public Login()
        {
            LogIn1();
        }

        //private string Decrypt(string Str)
        //{
        //    int i = 1;
        //    string Password = "";
        //    string S = "";

        //    try
        //    {
        //        for (i = 1; i <= Strings.Len(Str); i++)
        //        {
        //            if (Strings.Mid(Str, i, 1) != "?")
        //                S = S + Strings.Mid(Str, i, 1);
        //            else
        //            {
        //                Password = Password + Strings.Chr(System.Convert.ToInt32(S) - 7);
        //                S = "";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return Password;
        //}

        private void LogIn1()
        {
            SAPbobsCOM.Company oCompany/* TODO Change to default(_) if this is not a reference type */;
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
                oCompany = new SAPbobsCOM.Company();
                // Set connection properties
                switch (ConnectionString.DbServerType)
                {
                    case "dst_MSSQL":
                        {
                            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL;
                            break;
                        }
                    case "dst_DB_2":
                        {
                            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_DB_2;
                            break;
                        }

                    case "dst_SYBASE":
                        {
                            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_SYBASE;
                            break;
                        }

                    case "dst_MSSQL2005":
                        {
                            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005;
                            break;
                        }

                    case "dst_MAXDB":
                        {
                            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MAXDB;
                            break;
                        }

                    case "dst_MSSQL2008":
                        {
                            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                            break;
                        }

                    case "dst_MSSQL2012":
                        {
                            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                            break;
                        }

                    case "dst_MSSQL2014":
                        {
                            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                            break;
                        }

                    case "HANADB":
                        {
                            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB;
                            break;
                        }
                }

                //string tmpstr;
                oCompany.Server = ConnectionString.Server;
                //tmpstr = oCompany.Server;
                oCompany.LicenseServer = ConnectionString.LicenseServer;
                //tmpstr = oCompany.LicenseServer;
                oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English; // change to your language
                oCompany.UseTrusted = false;
                oCompany.DbUserName = ConnectionString.DbUserName;
                //tmpstr = oCompany.DbUserName;
                oCompany.DbPassword = ConnectionString.DbPassword;
                oCompany.CompanyDB = ConnectionString.CompanyDB;
                oCompany.UserName = ConnectionString.UserName;
                oCompany.Password = ConnectionString.Password;
                oCompany.SLDServer = ConnectionString.SLDServer;

                if (oCompany.Connect() != 0)
                {
                    oCompany.GetLastError(out _lErrCode, out _sErrMsg);
                    _Company = null!;
                }
                else
                {
                    _lErrCode = 0;
                    _sErrMsg = "";
                    _Company = oCompany;
                }
            }
            catch (Exception ex)
            {
                _lErrCode = ex.GetHashCode();
                _sErrMsg = ex.Message;
                _Company = null!;
            }
        }
    }
}

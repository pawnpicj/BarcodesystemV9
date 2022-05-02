using System;
using System.Data;
using System.Data.Odbc;
using BarCodeAPIService.Connection;
using SAPbobsCOM;

namespace BarCodeAPIService.Models
{
    public class LoginOnlyDatabase
    {
        public LoginOnlyDatabase()
        {
            Login();
        }

        public OdbcDataAdapter AD { get; set; }

        public OdbcConnection CN { get; set; }

        public OdbcCommand CMD { get; set; }

        public string sErrMsg { get; private set; }

        public int lErrCode { get; private set; }

        public Company Company { get; internal set; }

        private void Login()
        {
            //string Server = "";
            //string DbUserName = "";
            //string DbPassword = "";
            //string CompanyDB = "";
            var connectionstr = "";
            try
            {
                connectionstr = "Driver={HDBODBC32};UID=" + ConnectionString.DbUserName + ";PWD=" +
                                ConnectionString.DbPassword + ";SERVERNODE=" + ConnectionString.Server + ";[DATABASE=" +
                                ConnectionString.CompanyDB + "];";

                CN = new OdbcConnection(connectionstr);

                if (CN.State == ConnectionState.Closed) CN.Open();

                if (CN.State == ConnectionState.Open) lErrCode = 0;
                else lErrCode = 9999;
            }
            catch (Exception ex)
            {
                lErrCode = ex.GetHashCode();
                sErrMsg = ex.Message;
            }
        }
    }
}
using BarCodeAPIService.Connection;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;

namespace BarCodeAPIService.Models
{
    public class LoginOnlyDatabase
    {
        private int _lErrCode;
        private string _sErrMsg;
        private OdbcCommand cmd;
        private OdbcConnection cn;
        private OdbcDataAdapter ad;

        public OdbcDataAdapter AD
        {
            get { return ad; }
            set { ad = value; }
        }
        public OdbcConnection CN
        {
            get { return cn; }
            set { cn = value; }
        }
        public OdbcCommand CMD
        {
            get { return cmd; }
            set { cmd = value; }
        }
        public string sErrMsg
        {
            get
            {
                return _sErrMsg;
            }
        }
        public int lErrCode
        {
            get
            {
                return _lErrCode;
            }
        }
        public LoginOnlyDatabase()
        {
            Login();
        }
        private void Login()
        {
            //string Server = "";
            //string DbUserName = "";
            //string DbPassword = "";
            //string CompanyDB = "";
            string connectionstr = "";
            try
            {

                connectionstr = "Driver={HDBODBC32};UID=" + ConnectionString.DbUserName + ";PWD=" + ConnectionString.DbPassword + ";SERVERNODE=" + ConnectionString.Server + ";[DATABASE=" + ConnectionString.CompanyDB + "];";
                CN = new OdbcConnection(connectionstr);
                if (CN.State == System.Data.ConnectionState.Closed) CN.Open();

                if (CN.State == System.Data.ConnectionState.Open) _lErrCode = 0;
                else _lErrCode = 9999;
            }
            catch (Exception ex)
            {
                _lErrCode = ex.GetHashCode();
                _sErrMsg = ex.Message;
            }

        }
    }
}
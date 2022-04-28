using System;
using System.Collections.Generic;
using System.Linq;
using SAPbobsCOM;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace BarCodeAPIService.Connection
{
    public class DefaultLogin
    {
        private int _lErrCode;
        private string _sErrMsg;
        private OdbcCommand cmd;
        private OdbcConnection cn;
        private OdbcDataAdapter ad;
        public OdbcCommand CMD
        {
            get { return cmd; }
            set { cmd = value; }
        }
        public OdbcConnection CN
        {
            get { return cn; }
            set { cn = value; }
        }
        public OdbcDataAdapter AD
        {
            get { return ad; }
            set { ad = value; }
        }
        public int lErrCode { get { return _lErrCode; } }
        public string sErrMsg { get { return _sErrMsg; } }
        public Company Company { get; internal set; }
        public DefaultLogin()
        {
            Login();
        }
        private void Login()
        {
            try
            {
                string connectionstr;
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

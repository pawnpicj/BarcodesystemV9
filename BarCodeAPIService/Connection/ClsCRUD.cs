using System;
using System.Data;
using System.Data.Odbc;

namespace BarCodeAPIService.Connection
{
    public class ClsCRUD
    {
        public string ErrMsg { get; set; }
        public DataTable GetDataWeb(string sql, string type)
        {
            var tb = new DataTable();
            var db = "";
            if (type == "WebDb") db = ConnectionString.ConnHana;
            var dtp = new OdbcDataAdapter(sql, db);
            try
            {
                dtp.Fill(tb);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tb = null;
                ErrMsg = ex.Message.ToString();
            }
             if(ErrMsg !=null)
            {
                tb = null;
            }
            return tb;
        }
    }
}
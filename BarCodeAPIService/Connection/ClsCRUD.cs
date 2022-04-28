using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;

namespace BarCodeAPIService.Connection
{
    public class ClsCRUD
    {
        public DataTable GetDataWeb(string sql,string type)
        {
            DataTable tb = new DataTable();
            string db = "";
            if (type == "WebDb")
            {
                db = ConnectionString.ConnHana;
            }            
            OdbcDataAdapter dtp = new OdbcDataAdapter(sql, db);
            try
            {
                dtp.Fill(tb);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tb = null;
            }
            return tb;
        }
    }
}

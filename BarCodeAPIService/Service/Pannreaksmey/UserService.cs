using BarCodeAPIService.Connection;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;
using BarCodeLibrary.Request.SAP.Pannreaksmey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using BarCodeAPIService.Models;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeAPIService.Service
{
    public class UserService : IUserService
    {
        public Task<ResponseOUSRGetUser> ResponseOUSRGetUser()
        {
            var oUSR = new List<OUSR>();
            DataTable dt = new DataTable();
            try
            {
                LoginOnlyDatabase login = new LoginOnlyDatabase();
                if (login.lErrCode == 0)
                {
                    String Query = "CALL \"" + ConnectionString.CompanyDB + "\"._USP_CALLTRANS_Smey ('OUSR','','','','','')";
                    login.AD = new System.Data.Odbc.OdbcDataAdapter(Query, login.CN);
                    login.AD.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        oUSR.Add(new OUSR
                        {
                            UserCode = row[0].ToString(),
                            UserName = row[1].ToString()
                        });
                    }
                    return Task.FromResult(new ResponseOUSRGetUser
                    {
                        ErrorCode = 0,
                        ErrorMessage = "",
                        Data = oUSR.ToList()
                    });
                }
                else
                {
                    return Task.FromResult(new ResponseOUSRGetUser
                    {
                        ErrorCode = login.lErrCode,
                        ErrorMessage = login.sErrMsg,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseOUSRGetUser
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        public Task<ResponsePostUser> ResponsePostUserAsync(SendUser send)
        {
            ClsCRUD clsCRUD = new ClsCRUD();
            var dt = clsCRUD.GetDataWeb("INSERT INTO \"" + ConnectionString.BarcodeDb + "\".TBUSER(USERCODE,USERNAME,PHONE,EMAIL,FAX,DEPARTMENT,PASSWORD,ADMINISTRATOR,CREATEDATE,UPDATEDATE,ACTIVE) VALUES( '"+send.UserCode+"','" + send.UserName +"','" + send.Phone + "','" + send.Email + "','" + send.Fax + "','" + send.Department + "','" + send.Password + "','" + send.Admin + "',(SELECT CURRENT_DATE  FROM DUMMY),'','" + send.Active + "');", "WebDb");
            if (dt != null)
            {
                return Task.FromResult(new ResponsePostUser
                {
                    ErrorCode = 0,
                    ErrorMsg = "",
                    UserCode = dt.Rows[0][1].ToString()
                }); 
            }
            else
            {
                return Task.FromResult(new ResponsePostUser {
                    ErrorCode = 0,
                    ErrorMsg = "",
                    UserCode =null
                });
            }
                                
        }  
        public Task<ResponseGetUser> RespponseGetuser()
        {
            ClsCRUD clsCRUD = new ClsCRUD();
            var tbUSER = new List<TBUSER>();
            var dt = clsCRUD.GetDataWeb("SELECT USERID,USERCODE,USERNAME,PHONE,EMAIL,FAX,DEPARTMENT,PASSWORD,ADMINISTRATOR,TO_DATE(CREATEDATE, 'YYYY-MM-DD'),TO_DATE(UPDATEDATE, 'YYYY-MM-DD'),ACTIVE FROM \"" + ConnectionString.BarcodeDb + "\".TBUSER", "WebDb");
            List<ResponseGetUser> responseGetUsers = new List<ResponseGetUser>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        tbUSER.Add(new TBUSER
                        {
                            UserID = row[0].ToString(),
                            UserCode = row[1].ToString(),
                            UserName = row[2].ToString(),
                            Phone = row[3].ToString(),
                            Email = row[4].ToString(),
                            Fax = row[5].ToString(),
                            Department = row[6].ToString(),
                            Password = row[7].ToString(),
                            Admin = row[8].ToString(),
                            CreateDate = row[9].ToString(),
                            UpdateDate = row[10].ToString(),
                            Active = row[11].ToString()
                        });
                    }
                    catch (Exception ex)
                    {
                        var e1 = ex.Message;
                    }
                }
                return Task.FromResult(new ResponseGetUser
                {
                    Data = tbUSER.ToList()
                });
            }
            else
            {
                return Task.FromResult(new ResponseGetUser
                {
                    Data = null
                }) ;
            }
        }
    }
}

using System.Collections.Generic;

namespace BarCodeLibrary.Respones.SAP.Pannreaksmey
{
    public class ResponseGetUser
    {
        public List<TBUSER> Data { get; set; }
    }

    public class TBUSER
    {
        public string UserID { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Department { get; set; }
        public string Password { get; set; }
        public string Admin { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string Active { get; set; }
    }
}
namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseInventoryCounting
    {
        public int ErrorCode { get; set; }

        public string? ErrorMsg { get; set; }

        //public string? DocNum { get; set; }
        public string? DocEntry { get; set; }
    }
}
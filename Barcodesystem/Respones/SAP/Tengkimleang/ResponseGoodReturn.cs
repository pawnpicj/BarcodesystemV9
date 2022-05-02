namespace BarCodeLibrary.Respones.SAP
{
    public class ResponseGoodReturn
    {
        public int ErrorCode { get; set; }
        public string? ErrorMsg { get; set; }
        public string? DocEntry { get; set; }
    }
}
namespace VideoGamesShop.Api.DataTransferObjects
{
    public class ShopRecordDetailsDto
    {
        public int RecordId { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeName { get; set; }
        public int VideoGameId { get; set; }
        public string Operation { get; set;}
    }
}

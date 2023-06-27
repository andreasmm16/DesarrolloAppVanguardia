namespace SmartMarket.Logic.DatetimeManager
{
    public class DatetimeService : IDatetimeService
    {
        public DateOnly GetDate()
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }
    }
}

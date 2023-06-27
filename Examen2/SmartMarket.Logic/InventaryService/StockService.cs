using SmartMarket.Logic.DatetimeManager;
using SmartMarket.Logic.Interfaces;

namespace SmartMarket.Logic.InventaryService;

public class StockService
{
    private readonly ISerializer stockSerializer;
    private readonly IDatetimeService datetime;
    private readonly IProviderService providerManagementService;

    public ISmartMarketDataAccess DataAccess { get; set; }


    public StockService(ISerializer stockSerializer, IDatetimeService datetime,IProviderService providerManagementService)
    {
        this.stockSerializer = stockSerializer;
        this.datetime = datetime;
        this.providerManagementService = providerManagementService;

    }
    public async Task<bool> AddStockItemAsync(string stockItem)
    {
        var stockItemObject = stockSerializer.Deserialize(stockItem);
        if (string.IsNullOrEmpty(stockItemObject.ProductName))
        {
            return false;
        }

        if (stockItemObject.Price <= 0)
        {
            return false;
        }

        var now = datetime.GetDate();
        var currentAge = now.DayNumber - stockItemObject.ProducedOn.DayNumber;
        switch (currentAge)
        {
            case > 30:
                return false;
            case > 15:
            case > 7 when stockItemObject.MembershipDeal is not null:
                stockItemObject.IsCloseToExpirationDate = true;
                break;
            default:
                stockItemObject.IsCloseToExpirationDate = false;
                break;
        }

        var provider = await providerManagementService.GetFromApiByIdAsync(stockItemObject.ProviderId);
        if (provider is null)
        {
            SmartMarketDataAccess.AddProvider(stockItemObject.ProviderId, stockItemObject.ProviderName);
        }
        
        return true;
    }
}
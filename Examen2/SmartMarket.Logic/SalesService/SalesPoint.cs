using SmartMarket.Logic.DatetimeManager;
using SmartMarket.Logic.Entities;
using SmartMarket.Logic.SalesService.DiscountsRules;

namespace SmartMarket.Logic.SalesService;

public class SalesPoint
{
    private readonly Dictionary<string, int> _productsInCart;
    private readonly IEnumerable<StockItem> _stock;
    private readonly IDatetimeService datetime;

    public SalesPoint(IEnumerable<StockItem> stock, IDatetimeService datetime)
    {
        _stock = stock;
        this.datetime = datetime;
        _productsInCart = new Dictionary<string, int>();
    }

    public void ScanItem(string productName)
    {
        var stockItem = _stock.FirstOrDefault(x => x.ProductName == productName);
        if (stockItem is null)
        {
            throw new ArgumentException($"Product {productName} not found in stock");
        }

        if (_productsInCart.TryGetValue(productName, out var quantity))
        {
            _productsInCart[productName] = quantity + 1;
        }
        else
        {
            _productsInCart.Add(productName, 1);
        }
    }

    public Dictionary<string, decimal> GetTotals()
    {
        var totals = new Dictionary<string, decimal>();
        foreach (var (product, quantity) in _productsInCart)
        {
            var today = datetime.GetDate();
            //Design Pattern: Rules
            var rules = new List<RuleBase>
            {
                new MonTuesDiscount(),
                new SaturdaySProductDiscount(),
                new NormalDiscount()
            };
            foreach(var rule in rules)
            {
                if (rule.IsMatch(today,product))
                {
                    var total = rule.AddTotal(product, quantity, _stock);
                    totals.Add(product, total);
                    break;
                }
            }
        }
        return totals;
    }
}
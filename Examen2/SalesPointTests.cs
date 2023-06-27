using Moq;
using SmartMarket.Logic.DatetimeManager;
using SmartMarket.Logic.Entities;
using SmartMarket.Logic.Interfaces;
using SmartMarket.Logic.InventaryService;
using SmartMarket.Logic.SalesService;

namespace SmartMarket.Tests;

public class SalesPointTests
{
    /*
     * Escanear un producto con una sola cantidad y sin ofertas de membresía retorna precio por cantidad.
     */
    [Fact]
    public void ScanningOneQuantityItem_NoMembershipDeals_ReturnsPricexQuantity()
    {
        // Arrange
        var stock = new List<StockItem>
        {
            new()
            {
                ProductName = "Milk",
                Price = 1.23m,
                ProducedOn = DateOnly.FromDateTime(DateTime.Now),
                ProviderId = Guid.NewGuid(),
                ProviderName = "Milk Provider"
            }
        };

        var datetimeMock = new Mock<IDatetimeService>();
        var fakeDate = new DateOnly(2023, 6, 21); 
        datetimeMock.Setup(d => d.GetDate()).Returns(fakeDate);

        var salesPoint = new SalesPoint(stock, datetimeMock.Object);
            // Act
            salesPoint.ScanItem("Milk");
        var totals = salesPoint.GetTotals();
        
        //Assert
        Assert.Equal(1.23m, totals["Milk"]);
    }
    
    /*
    * Escanear un producto mas de una vez y sin ofertas de membresía retorna precio por cantidad.
    */
    [Fact]
    public void ScanningOneItemMultipleTimes_NoMembershipDeals_ReturnsPricexQuantity()
    {
        // Arrange
        var stock = new List<StockItem>
        {
            new()
            {
                ProductName = "Milk",
                Price = 1.23m,
                ProducedOn = DateOnly.FromDateTime(DateTime.Now),
                ProviderId = Guid.NewGuid(),
                ProviderName = "Milk Provider"
            }
        };
        var datetimeMock = new Mock<IDatetimeService>();
        var fakeDate = new DateOnly(2023, 6, 21);  
        datetimeMock.Setup(d => d.GetDate()).Returns(fakeDate);

        var salesPoint = new SalesPoint(stock, datetimeMock.Object);

        // Act
        salesPoint.ScanItem("Milk");
        salesPoint.ScanItem("Milk");
        salesPoint.ScanItem("Milk");
        var totals = salesPoint.GetTotals();
        
        //Assert
        const decimal expectedTotal = 1.23m * 3;
        Assert.Equal(expectedTotal, totals["Milk"]);
    }
    
    /*
    * Escanear un producto mas de una vez con ofertas de membresía retorna precio de membresía.
    */
    [Fact]
    public void ScanningAnItem_WithMembershipDeal_ReturnsMembershipDealPrice()
    {
        // Arrange
        var stock = new List<StockItem>
        {
            new()
            {
                ProductName = "Milk",
                Price = 1.23m,
                ProducedOn = DateOnly.FromDateTime(DateTime.Now),
                ProviderId = Guid.NewGuid(),
                ProviderName = "Milk Provider",
                MembershipDeal = new MembershipDeal
                {
                    Price = 2.00m,
                    Quantity = 3,
                    Product = "Milk"
                }
            }
        };
        var datetimeMock = new Mock<IDatetimeService>();
        var fakeDate = new DateOnly(2023, 6, 21);
        datetimeMock.Setup(d => d.GetDate()).Returns(fakeDate);

        var salesPoint = new SalesPoint(stock, datetimeMock.Object);

        // Act
        salesPoint.ScanItem("Milk");
        salesPoint.ScanItem("Milk");
        salesPoint.ScanItem("Milk");
        var totals = salesPoint.GetTotals();
        
        //Assert
        Assert.Equal(2.00m, totals["Milk"]);
    }
    
    /*
    * Escanear un producto mas de una vez con ofertas de membresía y mas cantidad retorna precio de membresía mas precio por cantidad.
    */
    [Fact]
    public void ScanningAnItem_WithMembershipDealAndMoreQuantity_ReturnsMembershipDealPricePlusPriceByQuantity()
    {
        // Arrange
        var stock = new List<StockItem>
        {
            new()
            {
                ProductName = "Milk",
                Price = 1.23m,
                ProducedOn = DateOnly.FromDateTime(DateTime.Now),
                ProviderId = Guid.NewGuid(),
                ProviderName = "Milk Provider",
                MembershipDeal = new MembershipDeal
                {
                    Price = 2.00m,
                    Quantity = 3,
                    Product = "Milk"
                }
            }
        };
        //Necesito falsificar la dependencia IDatetime
        var datetimeMock = new Mock<IDatetimeService>();
        var fakeDate = new DateOnly(2023, 6, 21); // Aquí puedes definir la fecha que deseas enviar
        datetimeMock.Setup(d => d.GetDate()).Returns(fakeDate);

        var salesPoint = new SalesPoint(stock, datetimeMock.Object);

        // Act
        salesPoint.ScanItem("Milk");
        salesPoint.ScanItem("Milk");
        salesPoint.ScanItem("Milk");
        salesPoint.ScanItem("Milk");
        var totals = salesPoint.GetTotals();
        
        //Assert
        Assert.Equal(2.00m + 1.23m, totals["Milk"]);
    }

    [Fact]
    public void ScanningAnItem_OnMondayOrTuesday_ReturnsFivePercentDiscount()
    {
        //Act
        var stock = new List<StockItem>
        {
            new()
            {
                ProductName = "Milk",
                Price = 1.23m,
                ProducedOn = DateOnly.FromDateTime(DateTime.Now),
                ProviderId = Guid.NewGuid(),
                ProviderName = "Milk Provider",
                MembershipDeal = new MembershipDeal
                {
                    Price = 2.00m,
                    Quantity = 3,
                    Product = "Milk"
                }
            }
        };

        //Necesito falsificar la dependencia IDatetime
        var datetimeMock = new Mock<IDatetimeService>();
        var fakeDate = DateOnly.FromDateTime(DateTime.Now); 
        datetimeMock.Setup(d => d.GetDate()).Returns(fakeDate);

        var salesPoint = new SalesPoint(stock, datetimeMock.Object);

        // Act
        salesPoint.ScanItem("Milk");
        var totals = salesPoint.GetTotals();
        
        //Assert
        Assert.Equal(1.23m * 0.95m, totals["Milk"]);
    }

    
}
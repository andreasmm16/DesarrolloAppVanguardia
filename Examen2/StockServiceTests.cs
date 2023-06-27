using Moq;
using SmartMarket.Logic.DatetimeManager;
using SmartMarket.Logic.Interfaces;
using SmartMarket.Logic.InventaryService;
using SmartMarket.Logic.Entities;
using Moq.Protected;
using System.Net;
using FluentAssertions;
using System.IdentityModel.Tokens.Jwt;

namespace SmartMarket.Tests
{
    public class StockServiceTests
    {
        /*---Pruebas Unitarias del Módulo de Inventario---*/
        [Fact]
        public async Task AddStockItemAsync_EmptyProductName_ReturnsFalse()
        {
            //arrange
            var stockItem = new StockItem
            {
                ProductName = "",
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
            };

            var serializerMock = new Mock<ISerializer>();
            serializerMock.Setup(x => x.Deserialize(It.IsAny<string>())).Returns(stockItem);

            var datetimeMock = new Mock<IDatetimeService>();
            var fakeDate = DateOnly.FromDateTime(DateTime.Now);
            datetimeMock.Setup(d => d.GetDate()).Returns(fakeDate);

            var providerMock = new Mock<IProviderService>();
            providerMock.Setup(x => x.GetFromApiByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Provider)null);

            var stockService = new StockService(serializerMock.Object, datetimeMock.Object, providerMock.Object);

            // Act
            var result = await stockService.AddStockItemAsync("");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AddStockItemAsync_InvalidPriceObject_ReturnsFalse()
        {
            //arrange
            //Precio debe ser menor o igual a cero
            var stockItem = new StockItem
            {
                ProductName = "Milk",
                Price = -1.23m,
                ProducedOn = DateOnly.FromDateTime(DateTime.Now),
                ProviderId = Guid.NewGuid(),
                ProviderName = "Milk Provider",
                MembershipDeal = new MembershipDeal
                {
                    Price = 2.00m,
                    Quantity = 3,
                    Product = "Milk"
                }
            };

            var serializerMock = new Mock<ISerializer>();
            serializerMock.Setup(x => x.Deserialize(It.IsAny<string>())).Returns(stockItem);

            var datetimeMock = new Mock<IDatetimeService>();
            var fakeDate = DateOnly.FromDateTime(DateTime.Now);
            datetimeMock.Setup(d => d.GetDate()).Returns(fakeDate);

            var providerMock = new Mock<IProviderService>();
            providerMock.Setup(x => x.GetFromApiByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Provider)null);

            var stockService = new StockService(serializerMock.Object, datetimeMock.Object, providerMock.Object);

            // Act
            var result = await stockService.AddStockItemAsync("");

            // Assert
            Assert.False(result);

        }

        [Fact]
        public async Task AddStockItemAsync_AgeOver_30Days_ReturnsFalse()
        {
            //arrange
            var stockItem = new StockItem
            {
                ProductName = "Milk",
                Price = 1.23m,
                ProducedOn = DateOnly.FromDateTime(DateTime.Now.AddDays(-31)),
                ProviderId = Guid.NewGuid(),
                ProviderName = "Milk Provider",
                MembershipDeal = new MembershipDeal
                {
                    Price = 2.00m,
                    Quantity = 3,
                    Product = "Milk"
                }
            };

            var serializerMock = new Mock<ISerializer>();
            serializerMock.Setup(x => x.Deserialize(It.IsAny<string>())).Returns(stockItem);

            var datetimeMock = new Mock<IDatetimeService>();
            var fakeDate = DateOnly.FromDateTime(DateTime.Now);
            datetimeMock.Setup(d => d.GetDate()).Returns(fakeDate);

            var providerMock = new Mock<IProviderService>();
            providerMock.Setup(x => x.GetFromApiByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Provider)null);

            var stockService = new StockService(serializerMock.Object, datetimeMock.Object, providerMock.Object);

            // Act
            var result = await stockService.AddStockItemAsync("");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AddStockItemAsync_CurrentAgeGreaterThan7_ReturnsTrueExpirationDate()
        {
            // Arrange
            var stockItem = new StockItem
            {
                ProductName = "Milk",
                Price = 1.23m,
                ProducedOn = DateOnly.FromDateTime(DateTime.Now.AddDays(-8)),
                ProviderId = Guid.NewGuid(),
                ProviderName = "Milk Provider",
                MembershipDeal = new MembershipDeal
                {
                    Price = 2.00m,
                    Quantity = 3,
                    Product = "Milk"
                }
            };

            //Crear un Provivar
            var provider = new Provider
            {
                Id = Guid.NewGuid(),
                Name = "Milk Provider",
            };

            var serializerMock = new Mock<ISerializer>();
            serializerMock.Setup(x => x.Deserialize(It.IsAny<string>())).Returns(stockItem);

            var datetimeMock = new Mock<IDatetimeService>();
            var fakeDate = DateOnly.FromDateTime(DateTime.Now);
            datetimeMock.Setup(d => d.GetDate()).Returns(fakeDate);

            var providerMock = new Mock<IProviderService>();
            providerMock.Setup(x => x.GetFromApiByIdAsync(It.IsAny<Guid>())).ReturnsAsync(provider);

            var stockService = new StockService(serializerMock.Object, datetimeMock.Object, providerMock.Object);

            // Act
            var result = await stockService.AddStockItemAsync("");

            // Assert
            Assert.True(stockItem.IsCloseToExpirationDate);
        }

        [Fact]
        public async Task AddStockItemAsync_DefaultAge_ReturnsFalseExpirationDate()
        {
            var stockItem = new StockItem
            {
                ProductName = "Milk",
                Price = 1.23m,
                ProducedOn = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
                ProviderId = Guid.NewGuid(),
                ProviderName = "Milk Provider",
                MembershipDeal = new MembershipDeal
                {
                    Price = 2.00m,
                    Quantity = 3,
                    Product = "Milk"
                }
            };

            //Crear un Provivar
            var provider = new Provider
            {
                Id = Guid.NewGuid(),
                Name = "Milk Provider",
            };

            var serializerMock = new Mock<ISerializer>();
            serializerMock.Setup(x => x.Deserialize(It.IsAny<string>())).Returns(stockItem);

            var datetimeMock = new Mock<IDatetimeService>();
            var fakeDate = DateOnly.FromDateTime(DateTime.Now);
            datetimeMock.Setup(d => d.GetDate()).Returns(fakeDate);

            var providerMock = new Mock<IProviderService>();
            providerMock.Setup(x => x.GetFromApiByIdAsync(It.IsAny<Guid>())).ReturnsAsync(provider);

            var stockService = new StockService(serializerMock.Object, datetimeMock.Object, providerMock.Object);

            // Act
            var result = await stockService.AddStockItemAsync("");

            // Assert
            Assert.False(stockItem.IsCloseToExpirationDate);
        }
    }
}

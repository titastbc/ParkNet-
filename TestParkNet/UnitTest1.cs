using Moq;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using ParkNet_Cristovao.Machado.Data.Services;

namespace TestParkNet
{
    public class UnitTest1
    {
        [Fact]
        public async Task CalculatePrice_ShouldReturnCorrectPrice()
        {
            // Arrange
            var startdate = DateTime.UtcNow.AddHours(-1);
            var _contextMock = new Mock<ApplicationDbContext>(); 
            var tariffticketRepositoryMock = new Mock<TariffTicketRepository>();

            var tariffTicketMock = new Mock<TariffTicket>();
            var tariffTickets = new List<TariffTicket>
            {
                new TariffTicket { Price = 10, Finalperiod = 60, Inicialperiod = 0 },
                new TariffTicket { Price = 3, Finalperiod = 120, Inicialperiod = 61 },
                new TariffTicket { Price = 1, Finalperiod = 180, Inicialperiod = 121 }
            };

            var _priceCalculator = new PriceCalculator(tariffticketRepositoryMock.Object, _contextMock.Object);

            tariffticketRepositoryMock.Setup(repository => repository.GetTariffTickets())
        .ReturnsAsync(tariffTickets);

            // Act
            var result = await _priceCalculator.CalculatePrice(startdate);

            // Assert
            Assert.Equal(10m, result); 

        }
    }
}
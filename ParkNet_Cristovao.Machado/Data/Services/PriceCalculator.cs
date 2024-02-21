using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using System;
using System.Linq;

namespace ParkNet_Cristovao.Machado.Data.Services
{
    public class PriceCalculator
    {
        private readonly TariffTicketRepository _tariffTicket;
        public PriceCalculator(TariffTicketRepository tariffTicket)
        {
            _tariffTicket = tariffTicket;
        }
        public decimal CalculatePrice(Ticket ticket)
        {
            var minutes = (ticket.StartDate - DateTime.Now).TotalMinutes;
            var tariffs = _tariffTicket.GetTariffTickets();
            var price = tariffs.Result[0].Price;
            int cout = 0;
            var RestMinutes = minutes;
            foreach (var tariff in tariffs.Result)
            {
                if (tariff == tariffs.Result[0])
                    continue;
                if (tariffs.Result.Last() == tariff)
                    break;
                if (minutes >= tariff.Finalperiod )
                {
                    price += tariff.Price;
                    RestMinutes -= tariff.Finalperiod;
                }
                else
                {
                    return price;
                }
            }
            price += ((decimal)RestMinutes * tariffs.Result.Last().Price);

        }
    }
}

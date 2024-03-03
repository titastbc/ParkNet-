using NuGet.Protocol.Core.Types;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;
using ParkNet_Cristovao.Machado.Data.Repositories;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Data.Services
{
    public class PriceCalculator
    {
        private readonly TariffTicketRepository _tariffTicket;
        private readonly DailyTicketTariff _dailyTicketTariff;
        private readonly ApplicationDbContext _context;
        private TicketRequestModel _ticketRequestModel;
        public PriceCalculator(TariffTicketRepository tariffTicket, ApplicationDbContext context)
        {
            _context = context;
            _ticketRequestModel = _context.TicketRequestModel
                .OrderBy(p => p.Id).LastOrDefault();
            _tariffTicket = tariffTicket;
            _dailyTicketTariff = _context.DailyTicketTariff.FirstOrDefault();
        }
        public async Task<decimal> CalculatePrice(DateTime startdate)
        {
            var minutes = (DateTime.UtcNow - startdate).TotalMinutes;
            var tariffs = await _tariffTicket.GetTariffTickets();

            if (tariffs == null || !tariffs.Any())
            {
                return 0;
            }

            var price = tariffs.First().Price;
            double restMinutes = minutes;

            foreach (var tariff in tariffs.Skip(1)) 
            {
                if (tariff == tariffs.Last())
                {
                    price += (decimal)(restMinutes / 15) * tariffs.Last().Price;
                }

                if (minutes >= tariff.Finalperiod)
                {
                    price += tariff.Price;
                    restMinutes -= (double)tariff.Finalperiod;
                }
                else
                {
                    return price;
                }
            }

            return price;
        }

        public decimal CalculateDailyPrice(string userid)
        {
            return _dailyTicketTariff.Price;
        }
        public decimal Profitt(int nummounths)
        {
            DateTime firstday = new DateTime();
            DateTime lastday = new DateTime();
            if(DateTime.Now.Month - nummounths < 0)
            {
                nummounths = 12 - nummounths;
                if(nummounths == 0)
                    firstday = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, 1);
                else
                firstday = new DateTime(DateTime.Now.Year - 1, nummounths, 1);
                lastday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }
            else if (DateTime.Now.Month - nummounths == 0)
            {
                firstday = new DateTime(DateTime.Now.Year - 1, 12, 1);
                lastday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }
            else
            {
                firstday = new DateTime(DateTime.Now.Year, DateTime.Now.Month - nummounths, 1);
                lastday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }
            var total = (decimal)_context.Transactions
                .Where(p => (p.Description == "Ticket"
                || p.Description == "Permit") &&
                p.Date >= firstday && p.Date <= lastday)
                .Sum(p => p.Value);
            return Math.Abs(total);

        }
    }
}

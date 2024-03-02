using NuGet.Protocol.Core.Types;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;
using ParkNet_Cristovao.Machado.Data.Repositories;
using System;
using System.Drawing;
using System.Linq;

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
        public decimal CalculatePrice(DateTime startdate)
        {
            var minutes = (DateTime.Now - startdate).TotalMinutes;
            var tariffs = _tariffTicket.GetTariffTickets();
            var price = tariffs.Result[0].Price;
            int cout = 0;
            var RestMinutes = minutes;
            foreach (var tariff in tariffs.Result)
            {
                if (tariff == tariffs.Result[0])
                    continue;
                if (tariffs.Result.Last() == tariff)
                    price += ((decimal)RestMinutes / 15) * tariffs.Result.Last().Price;
                if (minutes >= tariff.Finalperiod)
                {
                    price += tariff.Price;
                    RestMinutes -= (double)tariff.Finalperiod;
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

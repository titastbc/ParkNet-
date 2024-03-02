using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Models;

namespace ParkNet_Cristovao.Machado.Data.Entities
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Park> Park { get; set; }
        public  DbSet<Floor> Floor { get; set; }
        public DbSet<ParkingSpace> ParkingSpace { get; set; }
        public DbSet<Permit> Permit { get; set; }
        public DbSet<TariffPermit> TariffPermits { get; set; }
        public DbSet<TariffTicket> TariffTickets { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
       public DbSet<PermitRequestModel> PermitRequestModel { get; set; }
        public DbSet<TicketRequestModel> TicketRequestModel { get; set; }
        public DbSet<DailyTicketTariff> DailyTicketTariff { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

using ParkNet_Cristovao.Machado.Data.Entities;

namespace ParkNet_Cristovao.Machado.Data.Services
{
    public class InicialConfigurator
    {
        ApplicationDbContext _Context;
        public InicialConfigurator(ApplicationDbContext applicationDbContext)
        {
                _Context = applicationDbContext;
        }

        public void TariffPermitInicialConfig()
        {
            _Context.Database.EnsureCreated();
            _Context.TariffPermits.Add(new TariffPermit { Period = 1, Price = 1, Name = "Monthly" });
            _Context.TariffPermits.Add(new TariffPermit { Period = 3, Price = 3, Name = "TriMonthly" });
            _Context.TariffPermits.Add(new TariffPermit { Period = 6, Price = 6, Name = "Biannual" });
            _Context.TariffPermits.Add(new TariffPermit { Period = 12, Price = 12, Name = "Annual" });
            _Context.SaveChanges();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using ParkNet_Cristovao.Machado.Data.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TariffPermitRepository _tariffPermitRepository;
        private readonly ParkRepository _parkRepository;
        private readonly InicialConfigurator _inicialConfigurator;

        public IndexModel(ILogger<IndexModel> logger,
            TariffPermitRepository tariffPermitRepository,
            ParkRepository parkRepository, InicialConfigurator inicialConfigurator)
        {
            _inicialConfigurator = inicialConfigurator;
            _tariffPermitRepository = tariffPermitRepository;
            _logger = logger;
            _parkRepository = parkRepository;
        }

        public List<TariffPermit> TariffPermit { get; set; }
        public List<Park> Parks { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Parks = await _parkRepository.GetParks();
            TariffPermit = _tariffPermitRepository.GetAll();
            if(TariffPermit.Count ==0 || Parks.Count == 0)
            {
                _inicialConfigurator.TariffPermitInicialConfig();
            }
            return Page();

        }
    }
}

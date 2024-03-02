using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Services;
using System;

namespace ParkNet_Cristovao.Machado.Pages.Stats
{
    public class ProfittModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly PriceCalculator _priceCalculator;
        public ProfittModel(ApplicationDbContext context, PriceCalculator priceCalculator)
        {
            _context = context;
            _priceCalculator = priceCalculator;
        }
        public void OnGet()
        {
            
            numounths = new int[] { 1, 3, 6, 12 };
            ProfittValues = new decimal[numounths.Length];
            for(int num = 0; num < numounths.Length; num++)
            {
                ProfittValues[num] = _priceCalculator.Profitt(numounths[num]);
            }
        }

        public decimal[] ProfittValues { get; set; }
        public int[] numounths { get; set; }
        public void OnPost()
        {


        }
    }
}

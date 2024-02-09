using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ParkNet_Cristovao.Machado.Data.Entities
{
    public class Customer : IdentityUser
    {
        [StringLength(16)]
        public string BankCardNumber { get; set; }
        [StringLength(10)]
        public string DriverLicenseNumber { get; set; }
        [StringLength(8)]
        public string CC { get; set; }
    }
}

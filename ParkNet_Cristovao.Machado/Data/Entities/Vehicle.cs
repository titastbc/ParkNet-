using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ParkNet_Cristovao.Machado.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        [StringLength(20)]
        public string Factory { get; set; }
        [StringLength(10)]
        public string Type { get; set; }
        public Customer User { get; set; }
        public string UserId { get; set; }
    }
}


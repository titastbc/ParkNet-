using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ParkNet_Cristovao.Machado.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        [StringLength(20)]
        public string Marca { get; set; }
        [StringLength(10)]
        public string Tipo { get; set; }
        public Customer User { get; set; }
        public string UserId { get; set; }
    }
}


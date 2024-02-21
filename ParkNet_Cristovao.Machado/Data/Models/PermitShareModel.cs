using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkNet_Cristovao.Machado.Data.Models
{
    public class PermitShareModel
    {
        [Display(Name = "ParkingSpace Name")]
        public string ParkingSpaceName { get; set; }
        public string Plate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PermitId { get; set; }   
        
    }
}

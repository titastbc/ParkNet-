using System;
using System.ComponentModel.DataAnnotations;

namespace ParkNet_Cristovao.Machado.Data.Entities
{
    public class Ticket
    {
        public string Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public ParkingSpace ParkingSpace { get; set; }
        public int ParkingSpaceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TicketPrice { get; set; }
    }
}

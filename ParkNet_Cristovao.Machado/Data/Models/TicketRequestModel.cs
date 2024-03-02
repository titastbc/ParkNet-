using System;

namespace ParkNet_Cristovao.Machado.Data.Models
{
    public class TicketRequestModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public int Parkid { get; set; }
        public string Userid { get; set; }
        public bool IsDaily { get; set; }
    }
}

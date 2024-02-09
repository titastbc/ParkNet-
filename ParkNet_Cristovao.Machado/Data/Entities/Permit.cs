using System;

namespace ParkNet_Cristovao.Machado.Data.Entities
{
    public class Permit
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public ParkingSpace ParkingSpace { get; set; }
        public int ParkingSpaceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}

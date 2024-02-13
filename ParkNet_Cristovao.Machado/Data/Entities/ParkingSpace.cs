using System.ComponentModel.DataAnnotations;

namespace ParkNet_Cristovao.Machado.Data.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public Floor Floor { get; set; }
        public int FloorID { get; set; }
        public string Type { get; set; }
    }
}

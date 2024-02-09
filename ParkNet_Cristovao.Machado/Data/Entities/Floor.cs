using System.ComponentModel.DataAnnotations;

namespace ParkNet_Cristovao.Machado.Data.Entities
{
    public class Floor
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public string Layout { get; set; }
        public Park Park { get; set; }
        public int ParkId { get; set; }
    }
}

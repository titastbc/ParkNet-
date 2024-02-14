namespace ParkNet_Cristovao.Machado.Data.Models
{
    public class PermitRequestModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Period { get; set; }
        public int Parkid { get; set; }
        public string Userid { get; set; }
    }
}

using ParkNet_Cristovao.Machado.Data.Entities;

namespace ParkNet_Cristovao.Machado.Data.Repositories
{
    public class ParkingSpaceRepository
    {
        ApplicationDbContext _context;
        public ParkingSpaceRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public void AddParkingSpace(ParkingSpace parkingSpace)
        {
            _context.ParkingSpace.Add(parkingSpace);
            _context.SaveChanges();
        }

    }
}

using ParkNet_Cristovao.Machado.Data.Entities;
using SQLitePCL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Data.Repositories
{
    public class ParkingSpaceRepository
    {
        ApplicationDbContext _context;
        public ParkingSpaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddParkingSpaces(List<ParkingSpace> parkingSpace)
        {
            _context.AddRange(parkingSpace);
            _context.SaveChanges();
        }
        public string[] GetParkingSpaceNameByFloorId(int[] floorid)
        {
            List<ParkingSpace> parkingSpaces = new List<ParkingSpace>();
            List<ParkingSpace> parkingSpaces2 = new List<ParkingSpace>();
            foreach (var id in floorid)
            {
               parkingSpaces = _context.ParkingSpace.Where(p => p.FloorID == id).ToList();
                parkingSpaces2.AddRange(parkingSpaces);
            }
                string[] names = new string[parkingSpaces2.Count];
            for(int i = 0; i < names.Length; i++)
            {

                names[i] = parkingSpaces2[i].Name;
            }
            return names;
        }
        public async Task<ParkingSpace> Update(List<ParkingSpace> parkingSpace)
        {
            foreach (var p in parkingSpace)
            {
                _context.ParkingSpace.Attach(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return null;
        }
    }
}

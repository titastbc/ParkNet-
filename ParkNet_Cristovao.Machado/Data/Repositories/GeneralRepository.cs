using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Migrations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Data.Repositories
{
    public class GeneralRepository
    {
        public readonly ParkRepository _parkrepository;
        public readonly FloorRepository _FloorRepository;
        public readonly ParkingSpaceRepository _ParkingSpaceRepository;
        public GeneralRepository(ParkRepository parkrepository, FloorRepository floorRepository, ParkingSpaceRepository parkingSpaceRepository)
        {
            _parkrepository = parkrepository;
            _FloorRepository = floorRepository;
            _ParkingSpaceRepository = parkingSpaceRepository;
        }

        public async Task<Park> AddMultiEntitiesasync(Park park, string layout, List<Floor> floors, List<ParkingSpace> parkingSpaces)
        {
            _ParkingSpaceRepository.AddParkingSpaces(parkingSpaces);
            await _FloorRepository.AddMultiFloor(floors);
            await _parkrepository.AddPark(park, layout);
            return null;
        }

        public async Task<Park> UpdateMultiEntities(Park park, string layout, List<Floor> floors, List<ParkingSpace> parkings)
        {
            _parkrepository.UpdateParkAsync(park);
            await _FloorRepository.UpdateFloors(floors);
            await _ParkingSpaceRepository.Update(parkings);
            return null;
        }
    }
}

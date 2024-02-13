using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ParkNet_Cristovao.Machado.Data.Services
{
    public class Checker
    {
        public FloorRepository _floorRepository;
        private readonly ParkingSpaceRepository _ParkingSpaceRepository;
        public readonly VehicleRepository _vehicleRepository;


        public Checker(FloorRepository floorRepository, ParkingSpaceRepository parkingSpaceRepository,
            VehicleRepository vehicleRepository)
        {
            _floorRepository = floorRepository;
            _ParkingSpaceRepository = parkingSpaceRepository;
            _vehicleRepository = vehicleRepository;
        }


        public List<ParkingSpace> FreePlacesChekcer(List<ParkingSpace> spaces)
        {
            var fullist = new List<Ticket>();
            var fullist2 = new List<Permit>();
            List<int> places = new List<int>();
            foreach (var parkingSpace in spaces)
            {
                fullist.AddRange(_floorRepository._context.Ticket.Where(p => p.ParkingSpaceId == parkingSpace.Id).ToList());
                fullist2.AddRange(_floorRepository._context.Permit.Where(p => p.ParkingSpaceId > parkingSpace.Id).ToList());
            }
            foreach (var t in fullist)
                places.Add(t.ParkingSpaceId);

            foreach (var t in fullist2)
                places.Add(t.ParkingSpaceId);

            foreach (var s in spaces)
                foreach (var p in places)
                {
                    if (s.Id == p)
                        spaces.Remove(s);
                }

            return spaces;

        }
    }
}

using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
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


        public List<ParkingSpace> FreePlacesChekcer(List<Floor> floors, Vehicle vehicle)
        {
            List<ParkingSpace> list = new List<ParkingSpace>();
            
            foreach (var floor in floors)
            {
                var aux = _floorRepository._context.ParkingSpace.Where(p => p.FloorID == floor.Id).ToList();
                var count = aux.Count;
                for (int i = 0; i < count; i++)
                {
                    if (aux[i].Type == vehicle.Type)
                    {
                        list.Add(aux[i]);
                    }
                }
            }
            return PermitAndTicketChecker(list);
        }

        public List<ParkingSpace> PermitAndTicketChecker(List<ParkingSpace> parkingSpaces)
        {
            var list = new List<ParkingSpace>(parkingSpaces);

            for (int i = list.Count - 1; i >= 0; i--)
            {
                var parkingSpace = list[i];
                var ticket = _floorRepository._context.Ticket.FirstOrDefault(t => t.ParkingSpaceId == parkingSpace.Id);
                var permit = _floorRepository._context.Permit.FirstOrDefault(p => p.ParkingSpaceId == parkingSpace.Id);

                if(ticket == null)
                {

                }
               else if (ticket != null && ticket.EndDate > DateTime.Now)
                {
                    list.RemoveAt(i);
                }
                else if (ticket.EndDate == null)
                {
                    list.RemoveAt(i);
                }

                if(permit == null)
                {

                }
                else if (permit != null && permit.EndDate > DateTime.Now)
                {
                    list.RemoveAt(i);
                }
                else if (permit.EndDate == null)
                {
                    list.RemoveAt(i);
                }
            }
            return list;
        }
        public string[,] MatrizCheked(string[,] matriz, List<ParkingSpace> freeplaces)
        {
            var count = freeplaces.Count;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == " " || matriz[i, j] == "\n" || matriz[i,j] == null)
                        continue;

                    bool foundMatch = false;

                    for (int k = 0; k < count; k++)
                    {
                        if (matriz[i, j] == freeplaces[k].Name + " ")
                        {
                            matriz[i, j] = freeplaces[k].Name + "      ";
                            foundMatch = true;
                            break; 
                        }
                    }

                    if (!foundMatch)
                    {
                        matriz[i, j] = "X ";
                    }
                }
            }

            return matriz;
        }
        public bool ActiveTicketCheker(string userid)
        {
            var vehicles = _vehicleRepository.GetVehiclesByUserId(userid).Result;
            foreach (var vehicle in vehicles)
            {
                var ticket = _floorRepository._context.Ticket
                    .Where(p => p.VehicleId == vehicle.Id).OrderBy(v => v.Id).LastOrDefault();
                if (ticket != null && ticket.EndDate > DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

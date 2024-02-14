using NuGet.Protocol.Core.Types;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Migrations;
using ParkNet_Cristovao.Machado.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Data.Services
{
    public class LayoutGestorService
    {
        public FloorRepository _floorRepository;
        private readonly ParkingSpaceRepository _ParkingSpaceRepository;
        private readonly Checker _checker;
        public LayoutGestorService(FloorRepository floorRepository, ParkingSpaceRepository parkingSpaceRepository, Checker checker)
        {
            _floorRepository = floorRepository;
            _ParkingSpaceRepository = parkingSpaceRepository;
            _checker = checker;
        }
        public List<Floor> FloorBuilder(int parkid, string layout)
        {
            List<string> layouts = LayoutSpliter(layout);
            List<Floor> list = new List<Floor>();
            for (int i = 0; i < layouts.Count; i++)
            {
                Floor floor = new Floor();
                floor.ParkId = parkid;
                floor.Layout = layouts[i];
                floor.Name = "Floor " + (i + 1);
                list.Add(floor);
            }
            return list;
        }
        public List<string> LayoutSpliter(string layout)
        {
            var lines = layout.Split("\r");
            List<string> list = new List<string>();
            list.Add("");
            int count = 0;
            foreach (var line in lines)
            {
                if (line == "\n ")
                {
                    count++;
                    list.Add("");
                    continue;
                }
                list[count] += line;
            }
            return list;

        }
        public List<ParkingSpace> GetPlaces(string layout, List<int> floorids, List<string> names)
        {
            var lines = layout.Split("\r\n");
            List<ParkingSpace> list = new List<ParkingSpace>();
            int idindex = 0;
            int countlugar = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == " ")
                {
                    idindex++;
                    continue;
                }
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == 'M')
                    {
                        ParkingSpace parkingSpace = new ParkingSpace();
                        parkingSpace.FloorID = floorids[idindex];
                        parkingSpace.Name = names[countlugar];
                        parkingSpace.Type = "Motorcycle";
                        countlugar++;
                        list.Add(parkingSpace);
                    }
                    else if (lines[i][j] == 'C')
                    {
                        ParkingSpace parkingSpace = new ParkingSpace();
                        parkingSpace.FloorID = floorids[idindex];
                        parkingSpace.Name = names[countlugar];
                        parkingSpace.Type = "Car";
                        countlugar++;
                        list.Add(parkingSpace);
                    }
                }
            }
            return list;
        }

        public string[,] LayoutMatrizBuilder(List<Floor> floors)
        {
            int maxlenght = StringHelper.MaxLenghtFloors(floors);
            int maxwidht = StringHelper.MaxWidhtFloors(floors);
            string[,] matriz = new string[maxwidht, maxlenght];
            int countwidht = 0;
            foreach (var floor in floors)
            {
                var lines = floor.Layout.Split("\n");
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        matriz[countwidht, j] = lines[i][j].ToString();
                    }
                    countwidht++;
                }
            }
            return matriz;

        }
        public List<string> GetNames(string[] strs)
        {
            char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            List<string> list = new List<string>();
            int index = 0;
            int count = 1;
            int countlugar = 1;
            string letters = "";
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i] == " ")
                    continue;
                if (i == 27)
                {
                    count++;
                    index = 0;
                }
                for (int j = 0; j < strs[i].Length; j++)
                {
                    letters = "";
                    for (int k = 0; k < count; k++)
                    {
                        letters += Alphabet[index];
                    }
                    if (strs[i][j] == 'C' || strs[i][j] == 'M')
                    {
                        list.Add(letters + countlugar);
                        countlugar++;

                    }
                }
                index++;
                countlugar = 1;
            }
            return list;
        }
        public string[,] LayouFromBd(int parkid)
        {
            var Floor = _floorRepository._context.Floor.Where(f => f.ParkId == parkid).ToList();
            
            string[,] matriz =  LayoutMatrizBuilder(Floor);
            int[] floorids = _floorRepository.GetFloorsId(Floor).ToArray();
            matriz = PlaceNamer(matriz, floorids);

            return matriz;
        }
        public string[,] PlaceNamer(string[,] matriz, int[] floorids)
        {

            var places = _ParkingSpaceRepository.GetParkingSpaceNameByFloorId(floorids);
            int count = 0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == "M" || matriz[i, j] == "C")
                    {
                        matriz[i, j] = places[count] + " ";
                        count++;
                    }
                }

            }
            return matriz;
        }
        public string[,] LayouFromBdWithFreeSpaces(int parkid, Vehicle vehicle)
        {
            var Floor = _floorRepository._context.Floor.Where(f => f.ParkId == parkid).ToList();

            string[,] matriz = LayoutMatrizBuilder(Floor);
            int[] floorids = _floorRepository.GetFloorsId(Floor).ToArray();
            List<ParkingSpace> places = new List<ParkingSpace>();
            foreach (var floor in Floor)
            {
                places.AddRange( _floorRepository._context.ParkingSpace.Where(p => p.FloorID == floor.Id).ToList());
            }
           places = _checker.FreePlacesChekcer(Floor, vehicle);
            matriz = PlaceNamer(matriz, floorids);
            matriz = _checker.MatrizCheked(matriz, places);

            return matriz;
        }
    }

}

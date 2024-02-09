using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Migrations;
using ParkNet_Cristovao.Machado.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Data.Services
{
    public class LayoutGestorService
    {
        public FloorRepository _floorRepository;
        public LayoutGestorService(FloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }
        public List<Floor> FloorBuilder(int parkid, string layout)
        {
            List<string> layouts = LayoutSpliter(layout);
            List<Floor> list = new List<Floor>();
            new List<ParkingSpace>();
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
        public List<ParkingSpace> GetPlaces(string layout, int floorid)
        {
            var lines = layout.Split("\r\n");
            List<ParkingSpace> list = new List<ParkingSpace>();
            List<string> names = GetNames(lines);
            char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == 'C' || lines[i][j] == 'M')
                    {
                        ParkingSpace parkingSpace = new ParkingSpace();
                        parkingSpace.FloorID = floorid;
                        parkingSpace.Name = names[j];
                        list.Add(parkingSpace);
                    }
                }
            }
            return list;
        }

        public char[,] LayoutMatrizBuilder(string layout)
        {
            var lines = layout.Split("\r\n");
            var lenght = StringHelper.MaxLenght(lines);
            char[,] matriz = new char[lines.Length, lenght];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matriz[i, j] = lines[i][j];
                }
            }
            return matriz;

        }
        public void PrintMatriz(char[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    System.Console.Write(matriz[i, j]);
                }
                System.Console.WriteLine();
            }
        }
        public List<string> GetNames(string[] strs)
        {
            char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            List<string> list = new List<string>();
            int index = 0;
            int count = 1;
            string letters = "";
            for (int i = 0; i < strs.Length; i++)
            {
                if (i == 27)
                {
                    count++;
                    index = 0;
                }
                for (int k = 0; k < count; k++)
                {
                    letters += Alphabet[index];
                    for (int j = 0; j < strs[i].Length; j++)
                    {
                        if (strs[i][j] == 'C' || strs[i][j] == 'M')
                        list.Add(letters + (j + 1));
                    }
                }
                index++;
            }
                return list;
        }
    }

}

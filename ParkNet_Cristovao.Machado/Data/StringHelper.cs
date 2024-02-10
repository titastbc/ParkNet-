using ParkNet_Cristovao.Machado.Data.Entities;
using System.Collections.Generic;

namespace ParkNet_Cristovao.Machado.Data
{
    public class StringHelper
    {

        public static int MaxLenght(string[] str)
        {
            int maxvalue = 0;
            foreach (var item in str)
            {
                if (item.Length > maxvalue)
                {
                    maxvalue = item.Length;
                }
            }
            return maxvalue;

        }
        public static int MaxLenghtFloors(List<Floor> floors)
        {

            int maxlenght = 0;
            int aux = 0;
            foreach (var floor in floors)
            {
                aux = StringHelper.MaxLenght(floor.Layout.Split("\n"));
                if (aux > maxlenght)
                {
                    maxlenght = aux;
                }

            }
            return maxlenght;
        }
        public static int MaxWidhtFloors(List<Floor> floors)
        {
            int maxwidht = 0;

            foreach (var floor in floors)
            {
                maxwidht += floor.Layout.Split("\n").Length;
            }
            return maxwidht;
        }
    }
}

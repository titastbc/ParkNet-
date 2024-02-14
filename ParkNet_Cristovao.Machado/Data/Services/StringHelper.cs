using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;
using ParkNet_Cristovao.Machado.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ParkNet_Cristovao.Machado.Data
{
    public class StringHelper
    {
        private readonly TariffPermitRepository _tariffPermit;

        public StringHelper(TariffPermitRepository tariffPermit)
        {
            _tariffPermit = tariffPermit;
        }
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
        public  List<string> PricesAndPeriodsToString()
        {
            var periods = _tariffPermit._context.TariffPermits.Select(p => p.Name).ToList();
            var prices = _tariffPermit._context.TariffPermits.Select(p => p.Price).ToList();
            var ids = _tariffPermit._context.TariffPermits.Select(p => p.Id).ToList();
            var select = new List<string>();
            for (int i = 0; i < periods.Count; i++)
            {
                select.Add(ids[i] + " - " + periods[i] + " - " + prices[i] + "$");
            }
            return select;
        }
    }
}

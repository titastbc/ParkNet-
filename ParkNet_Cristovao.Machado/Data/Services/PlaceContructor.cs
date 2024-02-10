using ParkNet_Cristovao.Machado.Data.Repositories;

namespace ParkNet_Cristovao.Machado.Data.Services
{
    public class PlaceContructor
    {
        private readonly ParkingSpaceRepository _ParkingSpaceRepository;
        public PlaceContructor(ParkingSpaceRepository parkingSpaceRepository)
        {
            _ParkingSpaceRepository = parkingSpaceRepository;
        }
        public string[,] PlaceBuilder(string[,] matriz, int[] floorids)
        {
            var places = _ParkingSpaceRepository.GetParkingSpaceNameByFloorId(floorids);
            int count = 0;
            for(int i = 0; i < matriz.GetLength(0); i++)
            {
                for(int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i,j] == "M" || matriz[i,j] == "C")
                    {
                        matriz[i, j] = places[count];
                        count++;
                    }
                }
                
            }
            return matriz;
        }
    }
}

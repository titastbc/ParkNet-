namespace ParkNet_Cristovao.Machado.Data
{
    public class StringHelper
    {

        public static int MaxLenght(string[] strings)
        {
            int max = 0;
            foreach (var s in strings)
            {
                if (s.Length > max)
                {
                    max = s.Length;
                }
            }
            return max;
        }
    }
}

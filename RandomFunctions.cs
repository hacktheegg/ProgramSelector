using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSelector
{
    public class RandomFunctions
    {
        public static void printPage(int pageNo)
        {
            string[] list = EncryptionDecryption.RetrieveDirectories().Split("\n");

            if (((pageNo * 7) + 7) <= list.Length)
            {
                for (int i = 0; i < 7; i++)
                {
                    Console.WriteLine((i + 1) + ". " + list[i + (pageNo * 7)].Split(@"\")[^1]);
                }
            }
            else if (((pageNo * 7) + 7) > list.Length)
            {
                for (int i = 0; i < (list.Length % 7); i++)
                {
                    Console.WriteLine((i + 1) + ". " + list[i + (pageNo * 7)].Split(@"\")[^1]);
                }
            }
        }

        public static int round(float number)
        {
            return (int)(number - (number % 1));
        }

        public static void RecompileDirectories()
        {

        }
    }
}

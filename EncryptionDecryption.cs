using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSelector
{
    class EncryptionDecryption
    {
        public static void AddDirectories(string directory, string whichLibrary)
        {

            //directory = directory.Replace(@"\", "z");
            string directories = RetrieveDirectories(whichLibrary);

            string encrypted;
            if (directories == "")
            {
                encrypted = stringEncryption.Encrypt((directories + directory), "hacktheegg");
            }
            else
            {
                encrypted = stringEncryption.Encrypt((directories + "\n" + directory), "hacktheegg");
            }


            StreamWriter sw = new(whichLibrary);
            sw.WriteLine(encrypted);
            sw.Close();
        }

        public static string RetrieveDirectories(string whichLibrary)
        {
            using (StreamWriter w = File.AppendText(whichLibrary))
            {
            }

            string temp = File.ReadAllText(whichLibrary);

            if (!string.IsNullOrEmpty(temp))
            {
                string directories = stringEncryption.Decrypt(temp, "hacktheegg");

                return directories;
            }

            return temp;
        }
    }
}

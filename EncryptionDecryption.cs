﻿using System;
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
        public static void AddDirectories(string directory)
        {
            //directory = directory.Replace(@"\", "z");
            string directories = RetrieveDirectories();
            //directories = directories.Replace(@"\", "z");
            //directories = directories.Replace("\n", "Z");

            string encrypted = stringEncryption.Encrypt((directories + "\n" + directory), "hacktheegg");

            StreamWriter sw = new("directories");
            sw.WriteLine(encrypted);
            sw.Close();
        }

        public static string RetrieveDirectories()
        {
            string temp = File.ReadAllText("directories");

            if (!string.IsNullOrEmpty(temp)) {
                string directories = stringEncryption.Decrypt(temp, "hacktheegg");
                //directories = directories.Replace("Z", "\n");

                return directories;
            }

            return temp;
        }
    }
}
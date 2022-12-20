﻿// See https://aka.ms/new-console-template for more information
using ProgramSelector;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello! Which Program do you want to see?");
        //for (int i = 0; i < list.Length; i++)
        //{
        //    Console.WriteLine(i + 1 + ". " + list[i].Split(@"\")[^1]);
        //}
        //Console.WriteLine("new. Add Directory");

        string[] list = EncryptionDecryption.RetrieveDirectories().Split("\n");

        bool answer = false;
        int pageNo = 0;

        while (!answer)
        {
            Console.WriteLine("Hello! Which Program do you want to see?");
            RandomFunctions.printPage(pageNo);

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            char key = keyInfo.KeyChar;

            if (char.IsDigit(key))
            {
                if (key.ToString() == "8")
                {
                    if (pageNo == 0)
                    {
                        pageNo = RandomFunctions.round((float)(list.Length / 7));
                    } else
                    {
                        pageNo--;
                    }
                }
                else if (key.ToString() == "9")
                {
                    if ((pageNo * 7) + 8 > list.Length)
                    {
                        pageNo = 0;
                    } else
                    {
                        pageNo++;
                    }
                }
                else
                {
                    answer = true;
                    Process p = new();
                    ProcessStartInfo pi = new()
                    {
                        UseShellExecute = true,
                        FileName = list[int.Parse(key.ToString()) + (pageNo * 7)]
                    };
                    p.StartInfo = pi;

                    p.Start();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Admin Menu's");
                Console.WriteLine("Input password or type 'back' to go back");
                Console.Write("Password: ");
                string inputPassword = Console.ReadLine();

                if (inputPassword != "back" && !Array.Exists(Passwords.userPasswords,
                    element => element == inputPassword))
                {
                    System.Environment.Exit(1);
                }
            }
            Console.Clear();
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //var choice = Int32.Parse(Console.ReadLine());
        /*var choice = Console.ReadLine();
        if (int.TryParse(choice, out _))
        {
            int choiceTemp = Int32.Parse(choice);
            Process p = new();
            ProcessStartInfo pi = new()
            {
                UseShellExecute = true,
                FileName = list[choiceTemp - 1]
            };
            p.StartInfo = pi;

            p.Start();
        }
        else if (choice == "new")
        {
            Console.Write("Password: ");
            if (Array.Exists(Passwords.userPasswords, element => element == Console.ReadLine()))
            {
                Console.WriteLine("paste directory");
                EncryptionDecryption.AddDirectories(Console.ReadLine());
            }
        }*/
    }
}




//string[] list = File.ReadAllLines("directories.txt");



//var name = Console.ReadLine();





/*
Process p = new Process();
ProcessStartInfo pi = new ProcessStartInfo();
pi.UseShellExecute = true;
pi.FileName = @"C:\Users\jarra\OneDrive\Desktop\Non-Desktop\Other\image\142f8342b8c030baf7a57a24d96d4312.png";
p.StartInfo = pi;

p.Start();
*/
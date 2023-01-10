// See https://aka.ms/new-console-template for more information
using ProgramSelector;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using static ProgramSelector.Passwords;
using System.Data.SQLite;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string folderPath = "data";
        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        string filePath = @"data\GameDirectories.db";
        if (!System.IO.File.Exists(filePath))
        {
            string connectionString = "Data Source=" + filePath + ";Version=3;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            string query = "CREATE TABLE Directories (id INTEGER, main TEXT)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        string[] previousLibraries = Array.Empty<string>();
        string[] whichLibrary = { "Directories", "main" };

        ChooseProgram(previousLibraries, whichLibrary);

        //@"C:\Windows\write.exe"
    }  


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static void ChooseProgram(string[] previousLibraries, string[] whichLibrary)
    {
        string[] list = EncryptionDecryption.RetrieveDirectories(whichLibrary);

        bool answer = false;
        int pageNo = 0;

        while (!answer)
        {
            list = EncryptionDecryption.RetrieveDirectories(whichLibrary);

            Console.Clear();

            if (previousLibraries == Array.Empty<string>())
            {
                Console.WriteLine("Hello! You are in " + whichLibrary[1] + ". Which Program do you want to see?");
            } else
            {
                Console.WriteLine("Hello! You are in " + string.Join(@"\", previousLibraries) + ". Which Program do you want to see?");
            }

            RandomFunctions.printPage(pageNo, whichLibrary);
            Console.WriteLine("8. back");
            Console.WriteLine("9. forward");
            Console.WriteLine("page " + (pageNo + 1) + " out of " + ((list.Length / 7) + 1));
            Console.WriteLine("\n" + whichLibrary);

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            char key = keyInfo.KeyChar;

            if (char.IsDigit(key))
            {
                if (key.ToString() == "8")
                {
                    if (pageNo == 0)
                    {
                        pageNo = RandomFunctions.round((float)(list.Length / 7));
                    }
                    else
                    {
                        pageNo--;
                    }
                }
                else if (key.ToString() == "9")
                {
                    if ((pageNo * 7) + 8 > list.Length)
                    {
                        pageNo = 0;
                    }
                    else
                    {
                        pageNo++;
                    }
                }
                else
                {
                    if (list[(pageNo*7) + int.Parse(key.ToString()) - 1].StartsWith("#"))
                    {
                        Array.Resize(ref previousLibraries, previousLibraries.Length + 1);
                        previousLibraries[previousLibraries.Length - 1] = list[(pageNo * 7) + int.Parse(key.ToString()) - 1].Split(@"\")[^1].Split(".")[^2];

                        whichLibrary[1] = list[(pageNo * 7) + int.Parse(key.ToString()) - 1];

                        ChooseProgram(previousLibraries, whichLibrary);
                    
                    } else
                    {
                        answer = true;
                        Process p = new();
                        ProcessStartInfo pi = new()
                        {
                            UseShellExecute = true,
                            FileName = list[int.Parse(key.ToString()) + (pageNo * 7) - 1]
                        };
                        p.StartInfo = pi;

                        p.Start();
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Admin Menu's");
                Console.WriteLine("Input password or type 'back' to go back");
                Console.Write("Password: ");
                string inputPassword = Console.ReadLine();

                Passwords.AdminUser adminUser = new Passwords.AdminUser();
                adminUser.userPasswords = new string[] { "user1", "user2", "user3" };
                adminUser.ownedLibrary = new string[][] {
                    new string[] { "libraryPlaceholder" },
                    new string[] { "libraryAlpha", "libraryBeta" },
                    new string[] { "libraryGamma", "libraryDelta" }
                };

                if (inputPassword != "back" && !Array.Exists(adminUser.userPasswords,
                    element => element == inputPassword))
                {
                    System.Environment.Exit(1);
                }
                else if (Array.Exists(adminUser.userPasswords, element => element == inputPassword))
                {
                    int index = Array.FindIndex(adminUser.userPasswords, element => element == inputPassword);

                    if ((index == 0) || (previousLibraries == Array.Empty<string>()) || Array.Exists(adminUser.ownedLibrary[index], element => element == previousLibraries[0]))
                    {
                        //adminMenu(inputPassword, whichLibrary);

                        string[] tempList =
                        {
                            @"d\d1",
                            @"d\d2",
                            @"c\c1",
                            @"c\c2",
                            @"c\c3",
                            @"c\c4",
                            @"c\c5",
                            @"c\c6",
                            @"c\c7",
                            @"b\b1",
                            @"b\b2",
                            @"b\b3",
                            @"b\b4",
                            @"b\b5",
                            @"b\b6",
                            @"b\b7",
                            @"a\a1",
                            @"a\a2",
                            @"a\a3",
                            @"a\a4",
                            @"a\a5",
                            @"a\a6",
                            @"a\a7"
                        };

                        EncryptionDecryption.SortDirectories(whichLibrary, tempList);

                        Console.WriteLine("admin menu requested");
                        Console.ReadKey();

                    } else
                    {
                        Console.WriteLine("password not valid for library");
                        Console.ReadKey();
                    }
                }
            }
            Console.Clear();
        }
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static void adminMenu(string inputPassword, string[] whichLibrary)
    {
        Console.Clear();

        RandomFunctions.printAdminMenu();
        Console.WriteLine();
        Console.WriteLine("options:");
        Console.WriteLine("  new");

        Passwords.AdminUser adminUser = new Passwords.AdminUser();
        adminUser.userPasswords = new string[] { "user1", "user2", "user3" };
        adminUser.ownedLibrary = new string[][] {
            new string[] { "libraryPlaceholder" },
            new string[] { "libraryAlpha", "libraryBeta" },
            new string[] { "libraryGamma", "libraryDelta" }
        };
    
        bool masterPassword = inputPassword == adminUser.userPasswords[0];

        if (masterPassword)
        {
            Console.WriteLine("  delete");
        }
        Console.WriteLine();
        string option = Console.ReadLine();

        if (option == "new")
        {
            Console.Clear();
            Console.WriteLine("paste directory (without quotes)");
            EncryptionDecryption.AddDirectories(Console.ReadLine(), whichLibrary);

        }
        else if (option == "delete" && masterPassword)
        {
            string[] list = EncryptionDecryption.RetrieveDirectories(whichLibrary);

            bool answer = false;
            int pageNo = 0;

            while (!answer)
            {
                
                Console.Clear();
                Console.WriteLine("Hello! Which Program do you want to delete?");
                RandomFunctions.printPage(pageNo, whichLibrary);
                Console.WriteLine("8. back");
                Console.WriteLine("9. forward");
                Console.WriteLine("page " + (pageNo + 1) + " out of " + ((list.Length / 7) + 1));

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char key = keyInfo.KeyChar;

                if (char.IsDigit(key))
                {
                    if (key.ToString() == "8")
                    {
                        if (pageNo == 0)
                        {
                            pageNo = RandomFunctions.round((float)(list.Length / 7));
                        }
                        else
                        {
                            pageNo--;
                        }
                    }
                    else if (key.ToString() == "9")
                    {
                        if ((pageNo * 7) + 8 > list.Length)
                        {
                            pageNo = 0;
                        }
                        else
                        {
                            pageNo++;
                        }
                    }
                    else
                    {
                        answer = true;
                        List<string> DirectoryList = new List<string>(list);
                        DirectoryList.RemoveAt(int.Parse(key.ToString()) + (pageNo * 7) - 1);
                        //list[int.Parse(key.ToString()) + (pageNo * 7)];

                        //list = string.Join("\n", DirectoryList);
                        File.WriteAllText("directories", stringEncryption.Encrypt
                            (string.Join("\n", DirectoryList), "hacktheegg"));

                        //stringEncryption.Encrypt((directories + directory), "hacktheegg");
                    }
                }
            }
        }
    }

    /*else if (choice == "new")
    {
        Console.Write("Password: ");
        if (Array.Exists(Passwords.userPasswords, element => element == Console.ReadLine()))
        {
            Console.WriteLine("paste directory");
            EncryptionDecryption.AddDirectories(Console.ReadLine());
        }
    }*/


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
    }*/
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
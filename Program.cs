// See https://aka.ms/new-console-template for more information
using ProgramSelector;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello! Which Program do you want to see?");

        string[] list = EncryptionDecryption.RetrieveDirectories().Split("\n");
        for (int i = 0; i < list.Length; i++)
        {
            Console.WriteLine(i + 1 + ". " + list[i].Split(@"\")[^1]);
        }
        Console.WriteLine("new. Add Directory");


        //var choice = Int32.Parse(Console.ReadLine());
        var choice = Console.ReadLine();
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
            Console.WriteLine("paste directory");
            EncryptionDecryption.AddDirectories(Console.ReadLine());
        }
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
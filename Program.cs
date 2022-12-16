// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Reflection.PortableExecutable;

Console.WriteLine("Hello! Which Image do you want to see?");

string[] list = File.ReadAllLines("directories.txt");

for (int i = 0; i < list.Length; i++)
{
    Console.WriteLine((i + 1) + ". " + list[i].Split(@"\")[list[i].Split(@"\").Length - 1]);
}

var choice = Int32.Parse(Console.ReadLine());



Process p = new Process();
ProcessStartInfo pi = new ProcessStartInfo();
pi.UseShellExecute = true;
pi.FileName = list[choice - 1];
p.StartInfo = pi;

p.Start();





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
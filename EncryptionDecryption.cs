using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
        public static void AddDirectories(string directory, string[] whichLibrary)
        {

            //directory = directory.Replace(@"\", "z");
            /*string directories = RetrieveDirectories(whichLibrary);

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
            sw.Close();*/


            if (directory.StartsWith("#"))
            {
                string connectionString = @"Data Source=data\GameDirectories.db;Version=3;";
                SQLiteConnection connection = new SQLiteConnection(connectionString);
                connection.Open();

                string query = "ALTER TABLE " + whichLibrary[0] + " ADD COLUMN " + directory.Split(@"#")[1] + " TEXT";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.ExecuteNonQuery();

                connection.Close();



                connectionString = @"Data Source=data\GameDirectories.db;Version=3;";
                connection = new SQLiteConnection(connectionString);
                connection.Open();



                query = "INSERT INTO " + whichLibrary[0] + " (" + whichLibrary[1] + ") VALUES (?)";
                command = new SQLiteCommand(query, connection);

                string temp = directory;

                command.Parameters.Add(new SQLiteParameter(whichLibrary[1], temp));

                command.ExecuteNonQuery();

                connection.Close();

            } else
            {
                string connectionString = @"Data Source=data\GameDirectories.db;Version=3;";
                SQLiteConnection connection = new SQLiteConnection(connectionString);
                connection.Open();

                string query = "INSERT INTO " + whichLibrary[0] + " (" + whichLibrary[1] + ") VALUES (?)";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                string temp = directory;

                command.Parameters.Add(new SQLiteParameter(whichLibrary[1], temp));

                command.ExecuteNonQuery();

                connection.Close();
            }
            
        }

        public static string[] RetrieveDirectories(string[] whichLibrary)
        {
            /*using (StreamWriter w = File.AppendText(whichLibrary))
            {
            }

            string temp = File.ReadAllText(whichLibrary);

            if (!string.IsNullOrEmpty(temp))
            {
                string directories = stringEncryption.Decrypt(temp, "hacktheegg");

                return directories;
            }

            return temp;*/



            string connectionString = @"Data Source=data\GameDirectories.db;Version=3;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            string query = "SELECT " + whichLibrary[1] + " FROM " + whichLibrary[0];
            SQLiteCommand command = new SQLiteCommand(query, connection);

            SQLiteDataReader reader = command.ExecuteReader();

            List<string> values = new List<string>();
            while (reader.Read())
            {
                values.Add(reader[whichLibrary[1]].ToString());
            }

            connection.Close();

            string[] list = values.ToArray();

            return list;
        }

        public static void SortDirectories(string[] whichLibrary, string[] list)
        {

            string connectionString;
            SQLiteConnection connection;

            int maxIdValue = 0;
            string[] temp = { whichLibrary[0], "id" };



            connectionString = @"Data Source=data\GameDirectories.db;Version=3;";
            connection = new SQLiteConnection(connectionString);

            string query = "SELECT * FROM " + whichLibrary[0] + " WHERE id = (SELECT max(id) FROM " + whichLibrary[0] + ")";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            connection.Open();

            SQLiteDataReader reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                AddIntDirectories(0, temp);
            }

            while (reader.Read())
            {
                //Console.WriteLine("{0}\t{1}", reader.GetInt32(0), reader.GetString(1));

                maxIdValue = reader.GetInt32(0);
            }

            command.Parameters.Clear();

            reader.Close();
            connection.Close();



            while (list.Length - 1 > maxIdValue)
            {
                AddIntDirectories(maxIdValue + 1, temp);

                maxIdValue++;
            }
            
            
            
            
            
            connectionString = @"Data Source=data\GameDirectories.db;Version=3;";
            connection = new SQLiteConnection(connectionString);

            connection.Open();

            for (int i = 0; i < list.Length; i++)
            {
                query = "UPDATE " + whichLibrary[0] + " SET " + whichLibrary[1] + " = '" + list[i] + "' WHERE id ='" + i + "'";
                command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }









            Console.WriteLine("code is executed");

            /*connectionString = @"Data Source=data\GameDirectories.db;Version=3;";
            connection = new SQLiteConnection(connectionString);
            connection.Open();*/

            /*for(int i = 0; i < list.Length; i++)
            {

                string query = "UPDATE " + whichLibrary[0] + " SET " + whichLibrary[1] + " = '" + list[i] + "' WHERE  id ='%'";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();

            }*/
            /*
            for (int i = 0; i < list.Length; i++)
            {
                command.Parameters.Add(new SQLiteParameter("@" + whichLibrary[1], list[i]));
                command.Parameters.Add(new SQLiteParameter("@id", i + 1));
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            */

            connection.Close();

        }

        public static void AddIntDirectories(int IntDirectory, string[] whichLibrary)
        {
            string connectionString = @"Data Source=data\GameDirectories.db;Version=3;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            string query = "INSERT INTO " + whichLibrary[0] + " (" + whichLibrary[1] + ") VALUES (?)";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            int temp = IntDirectory;

            command.Parameters.Add(new SQLiteParameter(whichLibrary[1], temp));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}

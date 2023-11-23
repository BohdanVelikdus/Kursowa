using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kursowa
{
    abstract class dbK
    {
        //public virtual void openBD() { 

        //}

        public abstract void writeToBD(string word);

        public abstract List<string> getInfoFromDB();


    }

    

    internal class countriesDB : dbK
    {
        public countriesDB(){}

        private string path = "CountriesDB.txt";
        public override void writeToBD(string word) {
            try
            {
                
                using (StreamWriter writer = File.AppendText(path))
                {
                                        
                    writer.WriteLine("\n" + "'"+word+"'");
                }
                countriesDB.optimizingDB(path); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, we have some problems");
            }
        } 
        // считываю базу даных 
        public override List<string> getInfoFromDB() {
            try
            {
                List<string> modifiedLines = new List<string>();

                // Считываем строки из файла
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Заменяем первый и последний символ

                        char[] charArray = line.ToCharArray();
                        charArray[0] = ' '; // Замените на то, что вам нужно для первого символа
                        charArray[charArray.Length - 1] =  ' '; // Замените на то, что вам нужно для последнего символа
                        string modifiedLine = new string(charArray);
                        modifiedLine = modifiedLine.Trim();
                        modifiedLines.Add(modifiedLine);    
                    }
                }

                return modifiedLines;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in reading BD countries");
                return new List<string> { };
                
            }
        }

        public static void optimizingDB(string path) {
            try
            {
                List<string> lines = new List<string>();
                HashSet<string> uniqueLines = new HashSet<string>(); // Используем HashSet для уникальных строк
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            if (!uniqueLines.Contains(line))
                            {
                                uniqueLines.Add(line);
                                lines.Add(line);
                            }
                        }
                    }
                }

                lines.Sort();

                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (var line in lines)
                    {
                        writer.WriteLine(line);
                    }
                }
                MessageBox.Show("DB is optimized");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in optimizing");
            }
        }
    }










    // Towary


    internal class MainDB : dbK {
        public MainDB() { }

        private string path = "MainDB.txt";
        public override void writeToBD(string word)
        {
            try
            {

                using (StreamWriter writer = File.AppendText(path))
                {

                    writer.WriteLine("\n" + word );
                }
                MainDB.optimizingDB(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, we have some problems");
            }
        }

        public override List<string> getInfoFromDB()
        {
            return new List<string>();
        }

        public void rewrite(Dictionary<int , FormedStringForDB> toWrite) {
            try
            {

                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (KeyValuePair<int, FormedStringForDB> kvp in toWrite) {
                        FormedStringForDB str = new FormedStringForDB(kvp.Value.Name, kvp.Value.Numer, kvp.Value.Country, kvp.Value.Kilkist, 0);
                        writer.WriteLine(str.write_to_DB());
                    }
                    
                }
                MainDB.optimizingDB(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, we have some problems");
            }
        }


        public Dictionary<int, FormedStringForDB> getInfo() {

            try
            {

                Dictionary< int,FormedStringForDB> dataLines = new Dictionary<int, FormedStringForDB>();

                // Считываем строки из файла
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    int n = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Заменяем первый и последний символ

                        FormedStringForDB str = FormedStringForDB.returnStringBD(line);


                        dataLines.Add(n, str);
                        n++;
                    }
                }

                return dataLines;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in reading BD countries");
                return new Dictionary<int, FormedStringForDB>{ };

            }
           
        }
        public static void optimizingDB(string path)
        {
            try
            {
                // Считываем строки из файла в список, удаляя пустые строки
                List<string> lines = new List<string>();
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            lines.Add(line);
                        }
                    }
                }

                // Записываем обновленные строки обратно в файл
                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (var line in lines)
                    {
                        writer.WriteLine(line);
                    }
                }

                Console.WriteLine("Пустые строки успешно удалены из файла.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kursowa
{
    public partial class Add : Window
    {
        public void createListBox() {
            Countries.Items.Clear();
            TextBox ts = new TextBox();
            ts.Width = 100;
            ts.Height = 100;
            ts.IsEnabled = true;
            Countries.Items.Add(ts);
            countriesDB DB = new countriesDB();
            List<string> list= DB.getInfoFromDB();
            foreach(var i in list) {
                Countries.Items.Add(i);
            }
            //using (StreamReader sr = new StreamReader("CountriesDB.txt"))
            //{
            //    string line;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        Countries.Items.Add(line.Substring(1, line.Length - 2));
            //    }
            //}
        }
        public Add()
        {
            InitializeComponent();
            createListBox();
        }

        //незначна проблема
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.F1))
            {
                FormControllClass.open_help();
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FormControllClass.open_help();
        }

        private void Add_click(object sender, RoutedEventArgs e)
        {
            FormControllClass.add_wind();
        }
        private void Edit_click(object sender, RoutedEventArgs e)
        {
            FormControllClass.edit_wind();
        }
        private void Delete_click(object sender, RoutedEventArgs e)
        {
            FormControllClass.delete_wind();
        }
        private void Sorting_click(object sender, RoutedEventArgs e)
        {
            FormControllClass.sorting_wind();
        }
        private void Input_click(object sender, RoutedEventArgs e)
        {
            FormControllClass.input_wind();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string country_t;
            string kilkist_t = "";
            int s;
            if (Countries.SelectedIndex != -1 && int.TryParse(kilk_t.Text, out s)) {
                if (Countries.SelectedIndex.Equals(0))
                {
                    country_t = Countries.SelectedItem.ToString().Split(":")[1].Trim(); //oskilky otrymuju elemnet textbox
                    //zapys do drugoji BD
                    countriesDB DB = new countriesDB();
                    DB.writeToBD(country_t);
                }
                else
                {
                    country_t =  Countries.SelectedItem.ToString().Trim();
                }
                kilkist_t = kilk_t.Text.Trim();
                FormedStringForDB word;
                if (name_t.Text.Trim() == "" && num_t.Text.Trim() == "")
                {
                    word = new FormedStringForDB(country_t, kilkist_t, 0);
                }
                else if (name_t.Text.Trim() != "" && num_t.Text.Trim() == "")
                {
                    word = new FormedStringForDB( name_t.Text.Trim(), country_t, kilkist_t, 0);
                }
                else if (name_t.Text.Trim() == "" && num_t.Text.Trim() != "")
                {
                    word = new FormedStringForDB(int.Parse(num_t.Text.Trim()), country_t, kilkist_t,0);
                }
                else
                {
                    word = new FormedStringForDB( name_t.Text.Trim(), num_t.Text.Trim(), country_t, kilkist_t,0);
                }

                MainDB mDB = new MainDB();
                mDB.writeToBD(word.write_to_DB());
                createListBox();
                MessageBox.Show("Товар успішоно додано!", "Успіх");
            }
            else {
                MessageBox.Show("Помилка введення значення!", "Помилка");
            }
            //записувати в базу даних потрібно в форматі "__"__"__"__, де __ - відповідні поля.
            //під час парсингу необхдно починати з першого індексу
            //що робити, якщо в назві є "" - ? 
            //замінити на іншний символ символ -- $ -- ? 
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
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
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
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







        static countriesDB DB = new countriesDB();
        List<string> list = DB.getInfoFromDB();
        public void createListBox()
        {
            Countries.Items.Clear();
            TextBox ts = new TextBox();
            ts.Width = 100;
            ts.Height = 100;
            ts.IsEnabled = true;
            Countries.Items.Add(ts);
            ComboBox myComboBox = (ComboBox)FindName("myComboBox");
            foreach (var i in list)
            {
                Countries.Items.Add(i);
                myComboBox.Items.Add(i);
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

        public Dictionary<int, FormedStringForDB> sorting(Dictionary<int, FormedStringForDB> mainDictionary) { 
            //Dictionary<int, FormedStringForDB> newDictionary = new Dictionary<int, FormedStringForDB>();
            string nameFind = ((TextBox)FindName("name")).Text.ToString().Trim();
            string serijnyjFind = ((TextBox)FindName("serijnyj")).Text.ToString().Trim();
            string countryFind = "";
            
            if (((ComboBox)FindName("myComboBox")).SelectedIndex != -1)
            {
                countryFind = ((ComboBox)FindName("myComboBox")).SelectedItem.ToString().Trim();
            }

            if (nameFind != "") {
                foreach(KeyValuePair<int, FormedStringForDB> kvp in mainDictionary){
                    if (!(kvp.Value.Name.Contains(nameFind))) {
                        mainDictionary.Remove(kvp.Key);
                    }
                }
            }
            if (serijnyjFind != "")
            {
                foreach (KeyValuePair<int, FormedStringForDB> kvp in mainDictionary)
                {
                    if (!(kvp.Value.Numer.Contains(serijnyjFind)))
                    {
                        mainDictionary.Remove(kvp.Key);
                    }
                }
            }
            if (countryFind != "") {
                foreach (KeyValuePair<int, FormedStringForDB> kvp in mainDictionary)
                {
                    if (!(kvp.Value.Country.Contains(countryFind)))
                    {
                        mainDictionary.Remove(kvp.Key);
                    }
                }
            }

            foreach (KeyValuePair<int, FormedStringForDB> kvp in mainDictionary)
            {
                if (!(( Convert.ToInt32(((TextBox)FindName("myLowerTextBox")).Text) <= Convert.ToInt32(kvp.Value.Kilkist)) && (Convert.ToInt32(kvp.Value.Kilkist)  <= Convert.ToInt32(((TextBox)FindName("myUpperTextBox")).Text))))
                {
                    mainDictionary.Remove(kvp.Key);
                }
            }

            return mainDictionary;
        }

        MainDB mainDB = new MainDB();
        public Dictionary<int, FormedStringForDB> dataB { get; set; }
        public int maxKilkist { get; set; }
        public  void renew() {
            ListView myListView = (ListView)FindName("myListView");
            dataB = new Dictionary<int, FormedStringForDB>();
            dataB = mainDB.getInfo();
            maxKilkist = Convert.ToInt32(dataB[0].Kilkist); 
            foreach (KeyValuePair<int, FormedStringForDB> kvp in dataB)
            {
                if (Convert.ToInt32(kvp.Value.Kilkist) > maxKilkist) {
                    maxKilkist = Convert.ToInt32(kvp.Value.Kilkist);
                }
               
            }
            var viewSource = new System.Windows.Data.CollectionViewSource();

            Dictionary<int, FormedStringForDB> toFilter = new Dictionary<int, FormedStringForDB>();
            foreach (KeyValuePair<int, FormedStringForDB> kvp in dataB) {
                toFilter.Add(kvp.Key, kvp.Value);
            }
            Dictionary<int, FormedStringForDB> filtered = sorting(toFilter);
            viewSource.Source = filtered;
            myListView.ItemsSource = viewSource.View;
            DataContext = this;
        }
        public Edit()
        {
            InitializeComponent();
            //ListView myListView = (ListView)FindName("myListView");
            createListBox();
            renew();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((ListView)FindName("myListView")).SelectedIndex != -1)
            {
                string country_t;
                string kilkist_t = "";
                int s;
                ComboBox Countries = (ComboBox)FindName("Countries");
                if (Countries.SelectedIndex != -1 && int.TryParse(kilk_t.Text, out s))
                {
                    if (Countries.SelectedIndex.Equals(0))
                    {
                        country_t = Countries.SelectedItem.ToString().Split(":")[1].Trim(); //oskilky otrymuju elemnet textbox
                                                                                            //zapys do drugoji BD
                        countriesDB DB = new countriesDB();
                        DB.writeToBD(country_t);
                    }
                    else
                    {
                        country_t = Countries.SelectedItem.ToString().Trim();
                    }
                    kilkist_t = kilk_t.Text.Trim();
                    FormedStringForDB word;
                    if (name_t.Text.Trim() == "" && num_t.Text.Trim() == "")
                    {
                        word = new FormedStringForDB(country_t, kilkist_t, 1);
                    }
                    else if (name_t.Text.Trim() != "" && num_t.Text.Trim() == "")
                    {
                        word = new FormedStringForDB(name_t.Text.Trim(), country_t, kilkist_t, 1);
                    }
                    else if (name_t.Text.Trim() == "" && num_t.Text.Trim() != "")
                    {
                        word = new FormedStringForDB(int.Parse(num_t.Text.Trim()), country_t, kilkist_t, 1);
                    }
                    else
                    {
                        word = new FormedStringForDB(name_t.Text.Trim(), num_t.Text.Trim(), country_t, kilkist_t, 1);
                    }

                    //MainDB mDB = new MainDB();
                    //mDB.writeToBD(word.write_to_DB());
                    createListBox();
                    dataB.Remove(keyToEdit);
                    dataB[keyToEdit] = word;

                    mainDB.rewrite(dataB);
                    MessageBox.Show("Товар успішоно вiдредагований!", "Успіх");

                    renew();
                }
                else
                {
                    MessageBox.Show("Обов`язково введiть кiлькiсть");
                }
            }
            else
            {
                MessageBox.Show("Error in choose");
            }
        }
        public int keyToEdit { get; set; }
        
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var myListView = (ListView)sender;
            if (myListView.SelectedItem != null) {
                //var selectedObject = (Dictionary<int,FormedStringForDB>)
                var selectedObject = (KeyValuePair<int, FormedStringForDB>)myListView.SelectedItem;
                keyToEdit = selectedObject.Key;
                var value = selectedObject.Value;
             
                name_t.Text = value.Name;
                num_t.Text = value.Numer;
                kilk_t.Text = value.Kilkist;
                string c = value.Country;
                int n = list.IndexOf(c);
                Countries.SelectedIndex = 1 + n;
                Debug.Write("+++");
            }
            
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            ComboBox myComboBox = (ComboBox)FindName("myComboBox");
            TextBox searchTextBox = sender as TextBox;
            string filter = searchTextBox.Text.ToLower();
            myComboBox.Items.Filter = item =>
            {
                if (item == null) return false;
                return item.ToString().ToLower().Contains(filter);
            };
            
        }

        //private void Search_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    //renew();
        //}

        //private void SearchCombobox_TextChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //renew();
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            renew();
        }
    }
}

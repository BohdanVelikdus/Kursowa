using HamburgerMenu;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Collections.Specialized;

namespace Kursowa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            actualize();
        }

        public void actualize() {
            TextBox tb = (TextBox)FindName("Output");
            tb.Text = "";
            MainDB DB = new MainDB();
            Dictionary<int, FormedStringForDB> mainF = DB.getInfo();

            Dictionary<string, int> Import = new Dictionary<string, int>();
            int maxExport = 0;
            string country = "";
            countriesDB ctrs = new countriesDB();
            List<string> allCount= ctrs.getInfoFromDB();
            foreach (var ctr in allCount) {
                Import.Add(ctr, 0);
            }
            foreach (KeyValuePair<int, FormedStringForDB> kvp in mainF)
            {
                Import[kvp.Value.Country] += Convert.ToInt32(kvp.Value.Kilkist);
            }
            foreach (KeyValuePair<string, int> pair in Import)
            {
                if (pair.Value > maxExport)
                {
                    maxExport = pair.Value;
                    country = pair.Key;
                }
            }

            foreach (KeyValuePair<string, int> pair in Import)
            {
                tb.Text += pair.Key + " " + pair.Value + "\n";
            }
            tb.Text += "The max export is in \n\t" + country + " " + maxExport;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            actualize();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (Keyboard.IsKeyDown(Key.F1)) {
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

        void logs() {
            Input w1 = new Input();
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Application.Current.Dispatcher.Invoke(() =>
            //{
                Input window = new Input(); 
            //});
        }
    }
}

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
using System.Windows.Shapes;

namespace Kursowa
{
    /// <summary>
    /// Логика взаимодействия для Input.xaml
    /// </summary>
    public partial class Input : Window
    {
        public Input()
        {
            InitializeComponent();
        }

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


    }
}

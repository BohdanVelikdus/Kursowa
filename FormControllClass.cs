using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursowa
{
    internal class FormControllClass
    {
        public static void open_help() {
            Window1 w1 = new Window1();
            w1.Show();
        }
        public static void add_wind()
        {
            Add w1 = new Add();
            w1.ShowDialog();
            //w1.Show();
        }
        public static void edit_wind()
        {
            Edit w1 = new Edit();
            //w1.ShowDialog();
            w1.Show();
        }
        public static void delete_wind()
        {
            Delete w1 = new Delete();
            w1.ShowDialog();
            //w1.Show();
        }
        public static void sorting_wind()
        {
            Sorting w1 = new Sorting();
            w1.ShowDialog();
            //w1.Show();
        }
        public static void input_wind()
        {
            Input w1 = new Input();
            w1.ShowDialog();
            //w1.Show();

        }
        public static void print_log(string s)
        {
            //Window1 w1 = new Window1();
            //w1.console_output.Text += s + "\n";
        }
    }
}

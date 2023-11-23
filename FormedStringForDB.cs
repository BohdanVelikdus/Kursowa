using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Kursowa
{
    public class FormedStringForDB 
    {
        private string _Name = "";
        private string _Numer = "";
        private string _Country = "";
        private string _Kilkist = "";

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Numer
        {
            get { return _Numer; }
            set { _Numer = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Numer = value; }
        }
        public string Kilkist
        {
            get { return _Kilkist; }
            set { _Kilkist = value; }
        }

        public FormedStringForDB(string name, string numer, string country, string kilkist, int isToBD){
            if (isToBD == 0)
            {
                _Name =  /*"#$@" +*/ name;
                _Numer = "#$@" + numer;
                _Country = "#$@" + country;
                _Kilkist = "#$@" + kilkist;
            }
            else {
                _Name =  /*"#$@" +*/ name;
                _Numer = /*"#$@" +*/ numer;
                _Country = /*"#$@" +*/ country;
                _Kilkist = /*"#$@" +*/ kilkist;
            }
            
        }

        
        public FormedStringForDB(string country, string kilkist, int isToBD) : this("___", "00000000", country, kilkist, isToBD) { 
        
        }

        public FormedStringForDB(int num,string country, string kilkist, int isToBD) : this("___", num.ToString() , country, kilkist, isToBD)
        {

        }
        public FormedStringForDB(string name, string country, string kilkist, int isToBD) : this(name, "000000", country, kilkist, isToBD)
        {

        }
        public string write_to_DB() {
            return _Name+_Numer+_Country+_Kilkist;
        }

        public static FormedStringForDB returnStringBD(string line) {
            string[] lines = line.Split("#$@");
            FormedStringForDB str = new FormedStringForDB(lines[0], lines[1], lines[2], lines[3],1);
            return str;
        }
        ~FormedStringForDB() {
            FormControllClass.print_log("Деструктор виконався!");
        }
    }
}

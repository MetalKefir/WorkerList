using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace WorkerList
{
    class Pay : INotifyPropertyChanged, IDataErrorInfo
    {
        private string month;
        private float? salary;

        public string Month
        {
            get { return month; }
            set
            {
                month = value;
                OnPropertyChanged("Month");
            }
        }
        public float? Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Salary":
                        if ( !Regex.Match(Salary.ToString(), @"^[0-9]*[.,]?[0-9]+$").Success & Salary != null)
                            return "Зарпалата дожна быть положительным числовым значанием";
                    break;
                }
                return String.Empty;
            }
        }


        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}

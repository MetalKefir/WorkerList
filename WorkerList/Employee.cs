using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkerList
{
    class Employee: INotifyPropertyChanged
    {
        private string surname;
        private string departnumber;
        private string position;
        private List<uint> salary;
        private uint midlesalary;

        public string Surname {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string DepartNumber
        {
            get { return departnumber; }
            set
            {
                departnumber = value;
                OnPropertyChanged("DepartNumber");
            }
        }
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }
        public List<uint> Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
            }
            
        }
        public uint MidleSalary
        {
            get
            {
                if (Salary == null)
                    return 0;
                foreach (var mountsalary in Salary)
                    midlesalary += mountsalary;

                return midlesalary/=(uint)Salary.Count;
            }
            set {}
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        
    }
}

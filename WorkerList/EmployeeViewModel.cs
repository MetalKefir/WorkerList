using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkerList
{
    class EmployeeViewModel : INotifyPropertyChanged
    {

        private Employee employee;
        public EmployeeViewModel(ref Employee _employee) => employee = _employee;
        public Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged("Emloyee");
            }
        }



        public string Surname
        {
            get { return employee.Surname; }
            set
            {
                employee.Surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string DepartNumber
        {
            get { return employee.DepartNumber; }
            set
            {
                employee.DepartNumber = value;
                OnPropertyChanged("DepartNumber");
            }
        }
        public string Position
        {
            get { return employee.Position; }
            set
            {
                employee.Position = value;
                OnPropertyChanged("Position");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

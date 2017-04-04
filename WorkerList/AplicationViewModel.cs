using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkerList
{
    class AplicationViewModel: INotifyPropertyChanged
    {
        private Employee selectedEmployee;

        public ObservableCollection<Employee> Employees { get; set; }
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmloyee");
            }
        }


        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        Employee employee = obj as Employee;
                        if (employee != null)
                        {
                            Employees.Remove(employee);
                        }
                    },
                    (obj) => Employees.Count > 0));
            }
        }

        public AplicationViewModel()
        {
            Employees = new ObservableCollection<Employee>
            {
                new Employee{Surname = "Морозов", Position = "кодер", DepartNumber = "1", Salary = null},
                new Employee{Surname = "Карпунин", Position = "кодер", DepartNumber = "1", Salary = null },
                new Employee{Surname = "Гатилов", Position = "кодер", DepartNumber = "1", Salary = null }
            };
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

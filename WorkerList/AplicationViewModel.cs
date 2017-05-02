using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;


namespace WorkerList
{
    class AplicationViewModel: INotifyPropertyChanged
    {
        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmloyee");
            }
        }

        public ObservableCollection<Employee> Employees { get; set; }

        
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        if (selectedEmployee != null)
                            Employees.Remove(selectedEmployee);
                        else throw new DataExeption("Ошибка удаления");
                    },
                    (obj) => Employees.Count > 0 && selectedEmployee!=null));
            }
        }

        private RelayCommand readfileCommand;
        public RelayCommand ReadFileCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {

                        
                    }));
            }
        }

        public AplicationViewModel()
        {
            Employees = new ObservableCollection<Employee>()
            {
                new Employee{Surname = "Морозов", Position = "кодер", DepartNumber = 1},
                //new Employee{Surname = "Карпунин", Position = "кодер", DepartNumber = 1 },
                //new Employee{Surname = "Гатилов", Position = "кодер", DepartNumber = 1 }
            };

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

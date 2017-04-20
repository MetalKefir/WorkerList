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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkerList
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void EmployeeAdd(object sender, RoutedEventArgs e)
        {
            List<uint> _Salary = new List<uint>();
            IEnumerable<uint> salary;
            _Salary.Add(Convert.ToUInt32(January.Text));
            _Salary.Add(Convert.ToUInt32(February.Text));
            _Salary.Add(Convert.ToUInt32(March.Text));
            _Salary.Add(Convert.ToUInt32(April.Text));
            _Salary.Add(Convert.ToUInt32(May.Text));
            _Salary.Add(Convert.ToUInt32(June.Text));
            _Salary.Add(Convert.ToUInt32(Jule.Text));
            _Salary.Add(Convert.ToUInt32(August.Text));
            _Salary.Add(Convert.ToUInt32(September.Text));
            _Salary.Add(Convert.ToUInt32(October.Text));
            _Salary.Add(Convert.ToUInt32(November.Text));
            _Salary.Add(Convert.ToUInt32(December.Text));

            Employee newemployee = new Employee() {Surname = _Surname.Text, DepartNumber=_Department.Text, Position=_Position.Text, Salary=_Salary};
            (DataContext as ObservableCollection<Employee>).Add(newemployee);
        }
    }
}

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
        private uint counterror = 0;
        private Employee addedemployee;
        public AddEmployee()
        {
            DataContext = addedemployee = new Employee() { Surname = "", Position = "", DepartNumber = null };
            InitializeComponent();

        }

        private void EmployeeAdd(object sender, RoutedEventArgs e)
        {
            (Owner.DataContext as AplicationViewModel).Employees.Add(addedemployee);
            MessageBox.Show("Сотрудник успешно добавлен");
            Close();
        }

        private void SalaryDataError(object sender, ValidationErrorEventArgs e)
        {
            if (ButtonOK.IsEnabled || (sender as TextBox).ToolTip != null || counterror != 1)
            {
                ButtonOK.IsEnabled = false;

                if ((sender as TextBox).ToolTip != null)
                    counterror++;
                else counterror--;
            }
            else ButtonOK.IsEnabled = true;
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            addedemployee = null;
            Close();
        }
    }
}

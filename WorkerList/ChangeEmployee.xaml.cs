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
using System.Text.RegularExpressions;

namespace WorkerList
{
    /// <summary>
    /// Логика взаимодействия для ChangeEmployee.xaml
    /// </summary>
    public partial class ChangeEmployee : Window
    {
        private uint counterror = 0;
        private Employee employeebackup;

        public ChangeEmployee()
        {
            InitializeComponent();
        }

        private void ChangeEmployeeOK(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Измения успешно внесены");
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

        private void CancleWin(object sender, RoutedEventArgs e)
        {
            Window_Closed(sender, e);
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            (DataContext as Employee).Position = employeebackup.Position;
            (DataContext as Employee).Surname = employeebackup.Surname;
            (DataContext as Employee).DepartNumber = employeebackup.DepartNumber;
            (DataContext as Employee).Salary = employeebackup.Salary;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => employeebackup = (DataContext as Employee).ShallowCopy();
    }
}

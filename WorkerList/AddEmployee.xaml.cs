using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WorkerList
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private uint counterror = 0;
        private Employee addedemployee;
        private bool cancletrig=true;
        public AddEmployee()
        {
            DataContext = addedemployee = new Employee() { Surname = "", Position = "", DepartNumber = null };
            InitializeComponent();

        }

        private void EmployeeAdd(object sender, RoutedEventArgs e)
        {
            (Owner.DataContext as AplicationViewModel).Employees.Add(addedemployee);
            MessageBox.Show("Сотрудник успешно добавлен","Успех",MessageBoxButton.OK,MessageBoxImage.Information);
            cancletrig = false;
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

        private void CancleClick(object sender, RoutedEventArgs e) => OnClosed(e);

        private void WindowClosed(object sender, EventArgs e) => Close();

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if (cancletrig)
                if (MessageBox.Show("Вы увернены, что хотите отменить добаление?", "Отмена", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    e.Cancel = true;
                else DataContext = addedemployee = null;  
        }
    }
}

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WorkerList
{
    /// <summary>
    /// Логика взаимодействия для ChangeEmployee.xaml
    /// </summary>
    public partial class ChangeEmployee : Window
    {
        private uint counterror = 0;
        private Employee employeebackup;
        private bool cancletrig = true;
        public ChangeEmployee()
        {
            InitializeComponent();
        }

        private void ChangeEmployeeOK(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Измения успешно внесены","Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void WindowLoaded(object sender, RoutedEventArgs e) => employeebackup = (DataContext as Employee).ShallowCopy();

        private void CancleClick(object sender, RoutedEventArgs e) => OnClosed(e);

        private void WindowClosed(object sender, EventArgs e) => Close();

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if(cancletrig)
                if (MessageBox.Show("Вы увернены, что хотите отменить изменение?\nВсе данные будут востановлены.", "Отмена", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    e.Cancel = true;
                else
                {
                    (DataContext as Employee).Position = employeebackup.Position;
                    (DataContext as Employee).Surname = employeebackup.Surname;
                    (DataContext as Employee).DepartNumber = employeebackup.DepartNumber;
                    (DataContext as Employee).Salary = employeebackup.Salary;
                    DataContext = null;
                }
        }
    }
}

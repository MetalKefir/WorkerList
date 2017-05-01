using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.IO;
using System.Text.RegularExpressions;

namespace WorkerList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new AplicationViewModel();
            list.Items.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));
        }

        private void ReadFile(object sender, RoutedEventArgs e)
        {
            string filePath;

            try
            {
                filePath = Data.GetFile();
            }
            catch (DataExeption exeption)
            {
                MessageBox.Show(exeption.Message);
                return;
            }

            var data = File.ReadAllLines(filePath);

            foreach (var workdata in data)
            {

                var divdata = workdata.Split(' ');

                try
                {
                    if (divdata.Length < 14)
                        throw new DataExeption("В строке файла не хватает данных");
                    
                    if(Regex.Match(divdata[0], @"[^A-Za-zА-яа-я$]").Success || divdata[0]==" ")
                       throw new DataExeption("Фамилия сотрудника заданна некоректно");

                    if (Regex.Match(divdata[14], @"[^A-Za-zА-яа-я$]").Success || divdata[14] == " ")
                        throw new DataExeption("Должность сотрудника заданна некоректно");

                    if (Regex.Match(divdata[1], @"[^0-9]").Success || divdata[1] == " ")
                        throw new DataExeption("Номер отдела задан некоректно");

                    for (short i = 2; i < 14; i++)
                        if (!Regex.Match(divdata[i], @"^[0-9]*[.,]?[0-9]+$").Success || divdata[1] == " ")
                            throw new DataExeption("Зарплата задана некоректно");
                }
                catch (DataExeption exeption)
                {
                    MessageBox.Show(exeption.Message);
                    return;
                }

                Employee employee = new Employee()
                {
                    Surname = divdata[0],
                    DepartNumber = Convert.ToUInt16(divdata[1]),
                    Position = divdata[14]
                };

                for (short i = 2; i < 14; i++)
                    employee.Salary[i - 2].Salary =(float)Convert.ToDouble(divdata[i]);

                if (!(DataContext as AplicationViewModel).Employees.Contains(employee))
                    (DataContext as AplicationViewModel).Employees.Add(employee);
 
            }

            list.Items.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));
        }

        private void RecordFile(object sender, RoutedEventArgs e)
        {
            Func<ObservableCollection<Pay>, string> ConvertSalary =
                (ObservableCollection<Pay> salary) =>
                {
                    StringBuilder salarystr = new StringBuilder();
                    foreach(var monthsalary in salary)
                    {
                        salarystr.Append((monthsalary.Salary==null ? "0": monthsalary.Salary.ToString()));
                        salarystr.Append(" ");
                    }
                  
                    return salarystr.Remove(salarystr.Length-1, 1).ToString();
                };

            string filePath;

            try
            {
                filePath = Data.GetFile();
            }
            catch(DataExeption exeption)
            {
                MessageBox.Show(exeption.Message);
                return;
            }

            using (var data = new StreamWriter(filePath, false))
            {
                foreach (var employee in (DataContext as AplicationViewModel).Employees)
                {
                    data.WriteLineAsync($"{employee.Surname} {employee.DepartNumber} {ConvertSalary(employee.Salary)} {employee.Position}");
                }
            }
        }

        private void ChangeEmployee(object sender, RoutedEventArgs e)
        {
            try
            {
                if (list.SelectedItem == null)
                    throw new Exception("выберите элемент");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
           
            ChangeEmployee win = new ChangeEmployee()
            {
                DataContext = (list.SelectedItem as Employee)
            };
            win.Owner = this;
            win.ShowDialog();

            list.Items.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));
        }

        private void AddEmployees(object sender, RoutedEventArgs e)
        {

            AddEmployee win = new AddEmployee();

            win.Owner = this;
            win.ShowDialog();

            list.Items.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));
        }

    }
}

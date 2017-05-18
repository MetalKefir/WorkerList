using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

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

        /// <summary>
        /// Проверка данных в строке из файла
        /// </summary>
        /// <param name="data">массив записей в строке файла</param>
        private void DataValidation(ref string[] data)
        {
            if (data.Length < 15)
                throw new DataExeption("В строке файла не хватает данных");

            if (Regex.Match(data[0], @"[^A-Za-zА-яа-я$]").Success)
                throw new DataExeption("Фамилия сотрудника заданна некоректно");

            if (Regex.Match(data[14], @"[^A-Za-zА-яа-я$]").Success)
                throw new DataExeption("Должность сотрудника заданна некоректно");

            if (Regex.Match(data[1], @"[^0-9]").Success)
                throw new DataExeption("Номер отдела задан некоректно");

            for (short i = 2; i < 14; i++)
                if (!Regex.Match(data[i], @"^[0-9]*[.,]?[0-9]+$").Success)
                    throw new DataExeption("Зарплата задана некоректно");
        }

        /// <summary>
        /// Приведение строки к нужному формату
        /// </summary>
        /// <param name="dataarray">строка изъята из файла</param>
        /// <returns>строка соотвествующая требованиям</returns>
        private string[] DividData(string[] dataarray)
        {
            StringBuilder cleardata = new StringBuilder();

            for (short i = 0; i < dataarray.Length; i++)
            {
                if (dataarray[i] != "")
                    cleardata.Append(dataarray[i] + " ");
            }
            var finishdata = cleardata.Remove(cleardata.Length - 1, 1).ToString().Split(' ');

            DataValidation(ref finishdata);

            return finishdata;
        }

        /// <summary>
        /// Получение всех строк из файла
        /// </summary>
        /// <returns>массив строк из файла</returns>
        private string[] FileRecords()
        {
            var filePath = Data.GetFile();
            return File.ReadAllLines(filePath);
        }

        /// <summary>
        /// Чтение из файла
        /// </summary>
        private void ReadFile(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((DataContext as AplicationViewModel).Employees == null)
                    throw new DataExeption("Список сотрудников не найден");

                var filedata = FileRecords();

                foreach (var record in filedata)
                {
                    var finishdata = DividData(record.Split(' '));

                    Employee employee = new Employee()
                    {
                        Surname = finishdata[0],
                        DepartNumber = Convert.ToUInt16(finishdata[1]),
                        Position = finishdata[14]
                    };

                    for (short i = 2; i < finishdata.Length - 1; i++)
                        employee.Salary[i - 2].Salary = (float)Convert.ToDouble(finishdata[i]);

                    if (!(DataContext as AplicationViewModel).Employees.Contains(employee))
                        (DataContext as AplicationViewModel).Employees.Add(employee);

                }
            }
            catch (DataExeption exeption)
            {
                MessageBox.Show(exeption.Message,"Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            list.Items.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));
        }

        /// <summary>
        /// Запись в файл
        /// </summary>
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
                if ((DataContext as AplicationViewModel).Employees == null)
                    throw new DataExeption("Список сотрудников не найден");
                filePath = Data.GetSaveFile();
            }
            catch(DataExeption exeption)
            {
                MessageBox.Show(exeption.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        /// <summary>
        /// Изменение данных о сотруднике
        /// </summary>
        private void ChangeEmployee(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((DataContext as AplicationViewModel).Employees == null)
                    throw new DataExeption("Список сотрудников не найден");
                if (list.SelectedItem == null)
                    throw new Exception("элемент не выбран\nВыбирете один элемент из списка.");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message,"Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           
            ChangeEmployee win = new ChangeEmployee()
            {
                DataContext = (list.SelectedItem as Employee),
                Owner = this
            };
           
            win.ShowDialog();

            list.Items.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));

        }

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        private void AddEmployees(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((DataContext as AplicationViewModel).Employees == null)
                    throw new DataExeption("Список сотрудников не найден");
            }
            catch (DataExeption exeption)
            {
                MessageBox.Show(exeption.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AddEmployee win = new AddEmployee()
            {
                Owner = this
            };

            win.ShowDialog();

            list.Items.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        private void Exit(object sender, RoutedEventArgs e) => OnClosed(e);

        /// <summary>
        /// Событие закрытия окна
        /// </summary>
        private void WindowClosed(object sender, EventArgs e) => Close();

        /// <summary>
        /// Событие до закрытия окна
        /// </summary>
        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Вы увернены, что хотите выйти из программы?\n Все не сохраненые данные будут потеряны.", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else DataContext = null;
        }

        /// <summary>
        /// Удаление записи о сторуднике
        /// </summary>
        private void Deleting(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Delete.Command.CanExecute(list.SelectedItem))
                    Delete.Command.Execute(list.SelectedItem);
            }
            catch(DataExeption exeption)
            {
                MessageBox.Show(exeption.Message,"Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}

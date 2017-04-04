﻿using System.Windows;
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
        }


        private void Change(object sender, RoutedEventArgs e)
        {
            Employee employee = list.SelectedItem as Employee;

            ChangeEmployee win = new ChangeEmployee()
            {
                DataContext = new EmployeeViewModel(ref employee)
            };
            win.ShowDialog();
        }

    }
}
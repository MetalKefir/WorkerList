using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System;
using System.Collections.ObjectModel;

namespace WorkerList
{
    class Employee : IDataErrorInfo, IEquatable<Employee>
    {
        private float? midlesalary;

        public string Surname { get; set; }
        public uint? DepartNumber { get; set; }
        public string Position { get; set; }
        public ObservableCollection<Pay> Salary { get; set; }

        Func<ObservableCollection<Pay>, ObservableCollection<Pay>, bool> SalaryEqual = 
            (ObservableCollection<Pay> one, ObservableCollection<Pay> two)=> 
            {
                for(int i=0; i<one.Count; i++)
                    if(one[i].Salary != two[i].Salary) return false;
                
                return true;
            };

        public float? MidleSalary
        {
            get
            {
                if (Salary == null)
                    return 0;
                midlesalary = 0;
                foreach (var mountsalary in Salary)
                    midlesalary += (mountsalary.Salary ==null ? 0 : mountsalary.Salary);

                return midlesalary /= Salary.Count;
            }
            set {}
        }

        public Employee()
        {
            Salary = new ObservableCollection<Pay>()
            {
                new Pay{Month="January",Salary=null},
                new Pay{Month="February",Salary=null},
                new Pay{Month="March",Salary=null},
                new Pay{Month="April",Salary=null},
                new Pay{Month="May",Salary=null},
                new Pay{Month="June",Salary=null},
                new Pay{Month="Jule",Salary=null},
                new Pay{Month="August",Salary=null},
                new Pay{Month="September",Salary=null},
                new Pay{Month="October",Salary=null},
                new Pay{Month="November",Salary=null},
                new Pay{Month="December",Salary=null}

            };
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "DepartNumber":
                        if (Regex.Match(DepartNumber.ToString(), @"[^0-9]").Success || DepartNumber==null)
                            return "Номер отдела должен быть числом";
                        break;
                    case "Surname":
                        if (Regex.Match(Surname, @"[^A-Za-zА-яа-я]").Success || Surname=="")
                            return "Фамилия не может содержать цифры или спецсимволы";
                        break;
                    case "Position":
                        if (Regex.Match(Position, @"[^A-Za-zА-яа-я]").Success || Position=="")
                            return "Должность не может содержать цифры или спецсимволы";
                        break;
                }
                return String.Empty;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public Employee ShallowCopy()
        {
            return (Employee)MemberwiseClone();
        }

        public bool Equals(Employee other)
        {
            if (Surname == other.Surname && DepartNumber == other.DepartNumber
                && Position == other.Position && SalaryEqual(Salary,other.Salary))
            {
                return true;
            } else return false;
        }
    }
}

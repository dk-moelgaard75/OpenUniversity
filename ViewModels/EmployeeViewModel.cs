using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OpenUniversity.Commands;
using OpenUniversity.Models;
using OpenUniversity.Repository;
using OpenUniversity.Utility;

namespace OpenUniversity.ViewModels
{
    class EmployeeViewModel : ViewModelBase
    {
        #region private fields
        private string _firstName;
        private string _lastName;
        private int _age;
        private int _montlyHours;
        private int _montlySalary;
        private string _currentEmployeeType;
        private string _status = "N/A";
        private Visibility _statusVisibility;
        private ObservableCollection<EmployeeModel> _employees;
        private EmployeeModel _currentEmployee;
        //private DatabaseRepository<EmployeeModel> baseRepositoryEmploye;
        private IBaseRepository<EmployeeModel> baseRepositoryEmploye;
        #endregion

        #region Properties
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged("LastName");
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged("Age");
            }
        }
        public int MonthlyHours
        {
            get { return _montlyHours; }
            set
            {
                _montlyHours = value;
                RaisePropertyChanged("MonthlyHours");
            }
        }
        public int MonthlySalary
        {
            get { return _montlySalary; }
            set
            {
                _montlySalary = value;
                RaisePropertyChanged("MonthlySalary");
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged("Status");
            }
        }
        //Handles visibility of statuslabel
        public Visibility StatusVisibility
        {
            get { return _statusVisibility; }
            set
            {
                _statusVisibility = value;
                RaisePropertyChanged("StatusVisibility");
            }
        }
        public ObservableCollection<EmployeeModel> Employees
        {
            get { return _employees; }
            set { _employees = value; }
        }
        
        public EmployeeModel CurrentEmployee
        {
            get { return _currentEmployee; }
            set
            {
                _currentEmployee = value;
                if (_currentEmployee != null)
                {
                    FirstName = _currentEmployee.FirstName;
                    LastName = _currentEmployee.LastName;
                    Age = _currentEmployee.Age;
                    MonthlyHours = _currentEmployee.MonthlyHours;
                    MonthlySalary = _currentEmployee.MonthlySalary;
                    CurrentEmployeeType = _currentEmployee.Type;
                    Status = "Eksisterende ansat valgt";
                    StatusVisibility = Visibility.Visible;
                }
                RaisePropertyChanged("CurrentEmployee");
            }
        }
        public List<string> EmployeeTypes
        {
            get
            {
                return PersonUtility.EmployeeTypes;
            }
        }
        public string CurrentEmployeeType
        {
            get
            {
                return _currentEmployeeType;
            }

            set
            {
                _currentEmployeeType = value;
                HandleEmployeType();
            }
       }

        #endregion
        private void HandleEmployeType()
        {
            MonthlyHours = PersonUtility.GetMonthlyHours(_currentEmployeeType);
            MonthlySalary = PersonUtility.GetMonthlySalery(_currentEmployeeType);
            RaisePropertyChanged("CurrentEmployeeType");
        }
        internal void HandleKeyDown(KeyEventArgs e)
        {
            Status = "";
            StatusVisibility = Visibility.Hidden;
        }
        #region CommandDelete
        public ICommand Delete
        {
            get
            {
                return new CommandHandler(DeleteEmployee);
            }
        }
        public void DeleteEmployee()
        {
            if (_currentEmployee == null)
            {
                Status = "Ingen medarbejder valgt";
                StatusVisibility = Visibility.Visible;
            }
            else
            {
                //delete current employee - from collection and DB
                var found = Employees.FirstOrDefault(x => x.Id == _currentEmployee.Id);
                Employees.Remove(found);
                baseRepositoryEmploye.Delete(_currentEmployee.Id);
            }
        }
        #endregion

        #region CommandUpdate
        public ICommand Update
        {
            get
            {
                return new CommandHandler(UpdateEmployee);
            }
        }
        public void UpdateEmployee()
        {
            if (_currentEmployee == null)
            {
                Status = "Ingen medarbejder valgt";
                StatusVisibility = Visibility.Visible;
            }
            else
            {
                //Update current employee
                _currentEmployee.FirstName = FirstName;
                _currentEmployee.LastName = LastName;
                _currentEmployee.Age = Age;
                _currentEmployee.MonthlyHours = MonthlyHours;
                _currentEmployee.MonthlySalary = MonthlySalary;
                _currentEmployee.Type = CurrentEmployeeType;
                Status = "Medarbejder opdateret";
                StatusVisibility = Visibility.Visible;
                baseRepositoryEmploye.Update(_currentEmployee);
                var found = Employees.FirstOrDefault(x => x.Id == _currentEmployee.Id);
                int i = Employees.IndexOf(found);
                Employees[i] = _currentEmployee;
                RaisePropertyChanged("Employees");
            }
        }
        #endregion
        #region CommandAdd
        public ICommand Add
        {
            get
            {
                return new CommandHandler(AddEmployee);
            }
            
        }
        public void AddEmployee()
        {
            EmployeeModel employee = new EmployeeModel();
            employee.FirstName = FirstName;
            employee.LastName = LastName;
            employee.Age = Age;
            employee.MonthlyHours = MonthlyHours;
            employee.MonthlySalary = MonthlySalary;
            employee.Type = CurrentEmployeeType;
            Status = "Medarbejder oprettet";
            StatusVisibility = Visibility.Visible;
            //added to local list - implement backend global list thru repository
            baseRepositoryEmploye.Insert(employee);
            Employees.Add(employee);

            //serialize to XML
            //https://stackoverflow.com/questions/11710770/saving-dynamically-created-objects-in-wpf
        }

        #endregion

        public EmployeeViewModel()
        {
            //Initially hides status label
            StatusVisibility = Visibility.Hidden;
            //Setting default epmloyee type
            CurrentEmployeeType = "Sekretær";
            MonthlyHours = PersonUtility.GetMonthlyHours(CurrentEmployeeType);
            MonthlySalary = PersonUtility.GetMonthlySalery(CurrentEmployeeType);
            //baseRepositoryEmploye = new DatabaseRepository<EmployeeModel>();
            baseRepositoryEmploye = RepositoryFactory.GetRepository<EmployeeModel>();
            //Initalizes employees
            _employees = new ObservableCollection<EmployeeModel>();
            foreach(EmployeeModel model in baseRepositoryEmploye.GetAll())
            {
                _employees.Add(model);
            }
        }
    }
}

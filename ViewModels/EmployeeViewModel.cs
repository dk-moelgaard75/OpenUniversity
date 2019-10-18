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


namespace OpenUniversity.ViewModels
{
    class EmployeeViewModel : ViewModelBase
    {
        #region private fields
        private string _firstName;
        private string _lastName;
        private int _age;
        private string _currentEmployeeType;
        private string _status = "N/A";
        private Visibility _statusVisibility;
        private ObservableCollection<EmployeeModel> _employees;
        private EmployeeModel _currentEmployee;
        private BaseRepository<PersonModel> baseRepositoryPerson;
        private BaseRepository<EmployeeModel> baseRepositoryEmploye;
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
                FirstName = _currentEmployee.FirstName;
                LastName = _currentEmployee.LastName;
                Age = _currentEmployee.Age;
                CurrentEmployeeType = _currentEmployee.Type;
                Status = "Eksisterende ansat valgt";
                StatusVisibility = Visibility.Visible;
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
                RaisePropertyChanged("CurrentEmployeeType");
            }
       }

        #endregion

        internal void HandleKeyDown(KeyEventArgs e)
        {
            Status = "";
            StatusVisibility = Visibility.Hidden;
        }

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
            employee.MonthlyHours = 0;
            employee.MonthlySalary = 0;
            employee.Type = CurrentEmployeeType;
            Status = "Medarbejder oprettet";
            StatusVisibility = Visibility.Visible;
            //added to local list - implement backend global list thru repository
            PersonModel personModel = (PersonModel)employee;
            EmployeeModel employeeModel = (EmployeeModel)employee;
            baseRepositoryPerson.Insert(personModel);
            baseRepositoryEmploye.Insert(employeeModel);
            Employees.Add(employee);

            //serialize to XML
            //https://stackoverflow.com/questions/11710770/saving-dynamically-created-objects-in-wpf
        }
        public EmployeeViewModel()
        {
            //Initially hides status label
            StatusVisibility = Visibility.Hidden;
            //Initalizes employees
            _employees = new ObservableCollection<EmployeeModel>();
            //Setting default epmloyee type
            CurrentEmployeeType = "Sekretær";
            baseRepositoryPerson = new BaseRepository<PersonModel>();
            baseRepositoryEmploye = new BaseRepository<EmployeeModel>();
        }
    }
}

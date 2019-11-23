using OpenUniversity.Commands;
using OpenUniversity.CustomExceptions;
using OpenUniversity.Models;
using OpenUniversity.Repository;
using OpenUniversity.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OpenUniversity.ViewModels
{
    class StudentViewModel :ViewModelBase
    {
        #region private fields
        private string _socialSecurityNumber;
        private string _firstName;
        private string _lastName;
        private int? _age;
        private string _address;
        private int? _zipcode;
        private string _hometown;
        private string _phonenumber;
        
        private string _status = "N/A";
        private ObservableCollection<StudentModel> _students;
        #endregion
        private Visibility _statusVisibility = Visibility.Visible;
        public Visibility StatusVisibility
        {
            get { return _statusVisibility; }
            set 
            { 
                _statusVisibility = value;
                //Check if this is necesssary 
                RaisePropertyChanged("StatusVisibilty");
            }
        }
        private StudentModel _currentStudent;
        private IBaseRepository<StudentModel> baseRepositoryStudent;

        public string SocialSecurityNumber 
        {
            get { return _socialSecurityNumber; }
            set
            {
                _socialSecurityNumber = value;
                RaisePropertyChanged("SocialSecurityNumber");
            }
        }
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
        public int? Age 
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged("Age");
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                RaisePropertyChanged("Address");
            }
        }

        public int? ZipCode 
        {
            get { return _zipcode; }
            set
            {
                _zipcode = value;
                RaisePropertyChanged("ZipCode");
            }
        }

        public string HomeTown 
        {
            get
            {
               return _hometown;
            }
            set
            {
                _hometown = value;
                RaisePropertyChanged("HomeTown");
            }
        }

        public string PhoneNumber 
        { 
            get { return _phonenumber; }
            set
            {
                _phonenumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }
        public bool ClearOnCRUD { get; set; }

        public StudentModel CurrentStudent
        {
            get
            {
                return _currentStudent;
            }
            set
            {
                _currentStudent = value;
                if (_currentStudent != null)
                {
                    //set properties
                    SocialSecurityNumber = _currentStudent.SocialSecurityNumber;
                    FirstName = _currentStudent.FirstName;
                    LastName = _currentStudent.LastName;
                    Age = _currentStudent.Age;
                    Address = _currentStudent.Address;
                    ZipCode = _currentStudent.ZipCode;
                    HomeTown = _currentStudent.Hometown;
                    PhoneNumber = _currentStudent.Phonenumber;
                }
                RaisePropertyChanged("CurrentStudent");
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

        public ICommand Add
        {
            get
            {
                return new CommandHandler(AddStudent);
            }
        }
        public ICommand Update
        {
            get
            {
                return new CommandHandler(UpdateStudent);
            }
        }
        public ICommand Delete
        {
            get
            {
                return new CommandHandler(DeleteStudent);
            }
        }
        public void AddStudent()
        {
            Status = "Opretter student";
            //check if the socialsecuritynumber is in use
            //first reload students
            ReloadStudents();
            if (Students.Count > 0)
            { 
                StudentModel tmpModel = Students.SingleOrDefault(x => x.SocialSecurityNumber == SocialSecurityNumber);
                if (tmpModel != null)
                {
                    Status = "Brugeren findes allerede";
                    StatusVisibility = Visibility.Visible;
                    return;
                }
            }
            try
            {
                StudentModel student = new StudentModel(SocialSecurityNumber);
                student.SocialSecurityNumber = SocialSecurityNumber;
                student.FirstName = FirstName;
                student.LastName = LastName;
                student.Age = Age;
                student.Address = Address;
                student.ZipCode = ZipCode;
                student.Hometown = HomeTown;
                student.Phonenumber = PhoneNumber;
                baseRepositoryStudent.Insert(student);
                Students.Add(student);
                Status = "Student oprettet";
            }
            catch (InvalidSocialSecurityNumberException exInvalid)
            {
                Status = exInvalid.Message;
                StatusVisibility = Visibility.Visible;
                RaisePropertyChanged("StatusVisibility");
            }
            if (ClearOnCRUD)
            {
                ClearFields();
            }
            RaisePropertyChanged("Students");


        }
        public void UpdateStudent()
        {
            if (_currentStudent == null)
            {
                Status = "Ingen studerende valgt";
                StatusVisibility = Visibility.Visible;
            }
            else
            {
                //Update current employee
                _currentStudent.SocialSecurityNumber = SocialSecurityNumber;
                _currentStudent.FirstName = FirstName;
                _currentStudent.LastName = LastName;
                _currentStudent.Age = Age;
                _currentStudent.Address = Address;
                _currentStudent.ZipCode = ZipCode;
                _currentStudent.Hometown = HomeTown;
                _currentStudent.Phonenumber = PhoneNumber;

                Status = "Studerende opdateret";
                StatusVisibility = Visibility.Visible;
                baseRepositoryStudent.Update(_currentStudent);
                StudentModel found = Students.FirstOrDefault(x => x.Id == _currentStudent.Id);
                int i = Students.IndexOf(found);
                Students[i] = _currentStudent;
                RaisePropertyChanged("Students");
            }
            if (ClearOnCRUD)
            {
                ClearFields();
            }

        }
        public void DeleteStudent()
        {
            if (_currentStudent == null)
            {
                Status = "Ingen studerende valgt";
                StatusVisibility = Visibility.Visible;
            }
            else
            {
                //delete current employee - from collection and DB
                StudentModel found = Students.FirstOrDefault(x => x.Id == _currentStudent.Id);
                baseRepositoryStudent.Delete(_currentStudent.Id);
                Students.Remove(found);
                
            }
            if (ClearOnCRUD)
            {
                ClearFields();
            }
            RaisePropertyChanged("Students");
        }
        private void ClearFields()
        {
            SocialSecurityNumber = "";
            FirstName = "";
            LastName = "";
            Age = null;
            Address = "";
            ZipCode = null;
            HomeTown = "";
            PhoneNumber = "";

        }
        public ObservableCollection<StudentModel> Students 
        {
            get { return _students; }
            set { _students = value; }
        }
        private void ReloadStudents()
        {
            Students = new ObservableCollection<StudentModel>();
            foreach (StudentModel student in baseRepositoryStudent.GetAll())
            {
                Students.Add(student);
            }
        }
        public StudentViewModel()
        {
            baseRepositoryStudent = RepositoryFactory.GetRepository<StudentModel>();
            ReloadStudents();
        }
    }
}

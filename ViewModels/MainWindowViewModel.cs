using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenUniversity.Models;
using OpenUniversity.Data;
using OpenUniversity.Repository;
using OpenUniversity.Utility;

namespace OpenUniversity.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<EmployeeModel> _employees = new ObservableCollection<EmployeeModel>();
        private ObservableCollection<StudentModel> _students = new ObservableCollection<StudentModel>();
        private ObservableCollection<CourseModel> _courses = new ObservableCollection<CourseModel>();
        private IBaseRepository<EmployeeModel> baseRepositoryEmploye;
        public ObservableCollection<EmployeeModel> Employees
        {
            get { return _employees; }
            set { _employees = value; }
        }
        public ObservableCollection<StudentModel> Students
        {
            get { return _students; }
            set { _students = value; }
        }
        public ObservableCollection<CourseModel> Courses
        {
            get { return _courses; }
            set { _courses = value; }
        }

        public int NrOfEmployees
        {
            get 
            { 
                return Employees.Count;
            }
        }
        public void UpdateCollections()
        {
            ResetCollections();
            RaisePropertyChanged("NrOfEmployees");
        }
        private void ResetCollections()
        {
            _employees.Clear();
            foreach (EmployeeModel model in baseRepositoryEmploye.GetAll())
            {
                _employees.Add(model);
            }
        }
        public MainWindowViewModel()
        {
            //baseRepositoryEmploye = new DatabaseRepository<EmployeeModel>();
            baseRepositoryEmploye = RepositoryFactory.GetRepository<EmployeeModel>();
            UpdateCollections();
        }
    }

}

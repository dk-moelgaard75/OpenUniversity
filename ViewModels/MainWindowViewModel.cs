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
        private IBaseRepository<StudentModel> baseRepositoryStudent;
        private IBaseRepository<CourseModel> baseRepositoryCourse;
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
            get { return Employees.Count; }
        }
        public int NrOfStudents
        {
            get { return Students.Count; }
        }
        public int NrOfCourses
        {
            get { return Courses.Count; }
        }
        public void UpdateCollections()
        {
            ResetCollections();
            RaisePropertyChanged("NrOfEmployees");
            RaisePropertyChanged("NrOfStudents");
            RaisePropertyChanged("NrOfCourses");
        }
        private void ResetCollections()
        {
            _employees.Clear();
            foreach (EmployeeModel model in baseRepositoryEmploye.GetAll())
            {
                _employees.Add(model);
            }
            _students.Clear();
            foreach (StudentModel model in baseRepositoryStudent.GetAll())
            {
                _students.Add(model);
            }
            _courses.Clear();
            foreach(CourseModel courses in baseRepositoryCourse.GetAll())
            {
                _courses.Add(courses);
            }
        }
        public MainWindowViewModel()
        {
            //baseRepositoryEmploye = new DatabaseRepository<EmployeeModel>();
            baseRepositoryEmploye = RepositoryFactory.GetRepository<EmployeeModel>();
            baseRepositoryStudent = RepositoryFactory.GetRepository<StudentModel>();
            baseRepositoryCourse = RepositoryFactory.GetRepository<CourseModel>();
            UpdateCollections();
        }
    }

}

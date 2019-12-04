using OpenUniversity.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OpenUniversity.Repository;
using OpenUniversity.Utility;
using OpenUniversity.Models;
using System.Collections.ObjectModel;

namespace OpenUniversity.ViewModels
{
    class CourseViewModel : ViewModelBase
    {
        #region private fields
        private Guid _courseID;
        private string _courseName;
        private int? _coursePrice;
        private string _teacher;
        private string _status = "N/A";
        private Visibility _statusVisibility;
        private IBaseRepository<CourseModel> baseRepositoryCourses;
        private IBaseRepository<EmployeeModel> baseRepositoryEmployees;
        private EmployeeModel _currentTeacher;
        private CourseModel _currentCourse;
        private ObservableCollection<EmployeeModel> _teachers;
        private ObservableCollection<CourseModel> _courses;
        #endregion
        #region Properties
        public string CourseName
        {
            get { return _courseName; }
            set
            {
                _courseName = value;
                RaisePropertyChanged("CourseName");
            }
        }
        public int? CoursePrice
        {
            get { return _coursePrice; }
            set
            {
                _coursePrice = value;
                RaisePropertyChanged("CoursePrice");
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

        public Visibility StatusVisibility
        {
            get { return _statusVisibility; }
            set
            {
                _statusVisibility = value;
                RaisePropertyChanged("StatusVisibility");
            }
        }
        private Guid CourseID
        {
            get
            {
                if (_courseID == Guid.Empty)
                {
                    _courseID = Guid.NewGuid();
                }
                return _courseID;
            }
            set
            {
                _courseID = value;
                RaisePropertyChanged("CourseID");
            }
        }
        public EmployeeModel CurrentTeacher
        {
            get { return _currentTeacher; }
            set
            {
                _currentTeacher = value;
                RaisePropertyChanged("CurrentTeacher");
            }
        }
        public CourseModel CurrentCourse
        {
            get { return _currentCourse; }
            set
            {
                _currentCourse = value;
                if (_currentCourse != null)
                {
                    CourseID = _currentCourse.CourseID;
                    CourseName = _currentCourse.Name;
                    CoursePrice = _currentCourse.Price;
                    CurrentTeacher = baseRepositoryEmployees.GetById(_currentCourse.TeacherId);
                }
                RaisePropertyChanged("CurrentCourse");
            }
        }

        #endregion

        #region Actions
        public ICommand Add
        {
            get
            {
                return new CommandHandler(AddCourse);
            }
        }

        public void AddCourse()
        {
            //Create Course
            CourseModel course = new CourseModel();
            course.Name = CourseName;

            course.TeacherId = _currentTeacher.EmployeeId;
            course.Price = CoursePrice;
            baseRepositoryCourses.Insert(course);
            Courses.Add(course);
            Status = "Kursus oprettet";
            StatusVisibility = Visibility.Visible;
        }
        public ICommand Update
        {
            get
            {
                return new CommandHandler(UpdateCourse);
            }
        }

        private void UpdateCourse()
        {
            if (_currentCourse == null)
            {
                Status = "Intet kursus valgt";
                StatusVisibility = Visibility.Visible;
            }
            else
            {
                _currentCourse.Name = CourseName;
                _currentCourse.Price = CoursePrice;
                _currentCourse.TeacherId = CurrentTeacher.EmployeeId;
                Status = "Kursus opdateret";
                StatusVisibility = Visibility.Visible;
                baseRepositoryCourses.Update(_currentCourse);
                var found = Courses.FirstOrDefault(x => x.Id == _currentCourse.Id);
                int i = Courses.IndexOf(found);
                Courses[i] = _currentCourse;
                RaisePropertyChanged("Courses");


            }
        }
        public ICommand Delete
        {
            get
            {
                return new CommandHandler(DeleteCourse);
            }
        }

        private void DeleteCourse()
        {
            if (_currentCourse == null)
            {
                Status = "Intet kursus valgt";
                StatusVisibility = Visibility.Visible;
            }
            else
            {
                //delete current employee - from collection and DB
                var found = Courses.FirstOrDefault(x => x.Id == _currentCourse.Id);
                Courses.Remove(found);
                baseRepositoryCourses.Delete(_currentCourse.Id);
            }

        }


        #endregion
        public CourseViewModel()
        {
            StatusVisibility = Visibility.Hidden;
            baseRepositoryEmployees = RepositoryFactory.GetRepository<EmployeeModel>(); //new DatabaseRepository<CourseViewModel>();
            baseRepositoryCourses = RepositoryFactory.GetRepository<CourseModel>();
            ReloadAll();
        }
        private void ReloadAll()
        {
            ReloadTeachers();
            ReloadCourses();
        }

        public ObservableCollection<CourseModel> Courses
        {
            get { return _courses; }
            set { _courses = value; }
        }
        private void ReloadCourses()
        {
            Courses = new ObservableCollection<CourseModel>();
            foreach (CourseModel course in baseRepositoryCourses.GetAll())
            {
                Courses.Add(course);
            }
        }
        public ObservableCollection<EmployeeModel> Teachers
        {
            get { return _teachers; }
            set { _teachers = value; }
        }
        private void ReloadTeachers()
        {
            Teachers = new ObservableCollection<EmployeeModel>();
            foreach (EmployeeModel employee in baseRepositoryEmployees.GetAll())
            {
                if (employee.Type.Equals("Underviser"))
                {
                    Teachers.Add(employee);
                }
            }
        }
    }
}

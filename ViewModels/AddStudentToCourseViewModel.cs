using OpenUniversity.Commands;
using OpenUniversity.Models;
using OpenUniversity.Repository;
using OpenUniversity.Utility;
using OpenUniversity.Data;
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
    class AddStudentToCourseViewModel : ViewModelBase
    {
        private IBaseRepository<StudentModel> baseRepositoryStudents;
        private IBaseRepository<CourseModel> baseRepositoryCourses;
        private ObservableCollection<StudentModel> _students;
        private ObservableCollection<CourseModel> _courses;
        private string _status = "N/A";
        private Visibility _statusVisibility;

        private CourseModel _currentCourse;
        public AddStudentToCourseViewModel()
        {
            baseRepositoryCourses = RepositoryFactory.GetRepository<CourseModel>();
            baseRepositoryStudents = RepositoryFactory.GetRepository<StudentModel>();
            ReloadStudents();
            ReloadCourses();

        }

        private void ReloadCourses()
        {
            _courses = new ObservableCollection<CourseModel>();
            foreach (CourseModel course in baseRepositoryCourses.GetAll())
            {
                _courses.Add(course);
            }

        }

        private void ReloadStudents()
        {
            _students = new ObservableCollection<StudentModel>();
            foreach (StudentModel student in baseRepositoryStudents.GetAll())
            {
                _students.Add(student);
            }

        }
        public ObservableCollection<CourseModel> Courses
        {
            get { return _courses; }
            set { _courses = value; }
        }
        public CourseModel CurrentCourse
        {
            get { return _currentCourse; }
            set
            {
                _currentCourse = value;
                HandleAttendingStudents();
                RaisePropertyChanged("CurrentCourse");
            }
        }

        public ObservableCollection<StudentModel> Students
        {
            get { return _students; }
            set { _students = value; }
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

        public ICommand Update
        {
            get
            {
                return new CommandHandler(UpdateStudents);
            }
        }
        private void HandleAttendingStudents()
        {
            ReloadStudents();
            foreach (StudentModel student in Students)
            {
                if (student.Courses.FirstOrDefault(x => x.Id == CurrentCourse.Id) != null)
                {
                    student.AttendingCourse = true;
                }
                else
                {
                    student.AttendingCourse = false;
                }
                student.RaisePropertyChanged("AttendingCourse");
            }
            
        }

        private void UpdateStudents()
        {
            int nrOfStudentAdded = 0;
            int nrOfStudentRemoved = 0;
            if (CurrentCourse != null)
            {                
                foreach (StudentModel student in Students)
                {
                    if (student.AttendingCourse)
                    {
                        baseRepositoryCourses.HandleLink(CurrentCourse, student,true);                        
                        nrOfStudentAdded++;
                    }
                    else
                    {
                        baseRepositoryCourses.HandleLink(CurrentCourse, student, false);
                        nrOfStudentRemoved++;
                    }                    
                }
                //string interpolation - new i C# 6 
                Status = $"Antal studerende tilføjet/fjernet:{nrOfStudentAdded}/{nrOfStudentRemoved}";
                StatusVisibility = Visibility.Visible;

            }
            else
            {
                Status = "Intet kursus valgt";
                StatusVisibility = Visibility.Visible;
            }
        }
    }
}

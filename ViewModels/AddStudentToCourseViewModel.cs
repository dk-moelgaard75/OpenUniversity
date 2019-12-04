using OpenUniversity.Models;
using OpenUniversity.Repository;
using OpenUniversity.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.ViewModels
{
    class AddStudentToCourseViewModel
    {
        private IBaseRepository<StudentModel> baseRepositoryStudents;
        private IBaseRepository<CourseModel> baseRepositoryCourses;
        private ObservableCollection<StudentModel> _students;
        private ObservableCollection<CourseModel> _courses;
        public AddStudentToCourseViewModel()
        {
            baseRepositoryCourses = RepositoryFactory.GetRepository<CourseModel>();
            baseRepositoryStudents = RepositoryFactory.GetRepository<StudentModel>();
        }
    }
}

using OpenUniversity.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenUniversity.ViewModels
{
    class CourseViewModel: ViewModelBase
    {
        #region private fields
        private Guid _courseID;
        private string _courseName;
        private string _teacher;
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
        public string Teacher
        {
            get { return _teacher; }
            set
            {
                _teacher = value;
                RaisePropertyChanged("Teacher");
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
            _courseID = Guid.NewGuid();
            

            

        }
        #endregion
    }

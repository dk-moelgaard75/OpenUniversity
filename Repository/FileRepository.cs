using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenUniversity.Data;
using OpenUniversity.Models;
using OpenUniversity.Utility;

namespace OpenUniversity.Repository
{
    class FileRepository<T> : IBaseRepository<T> where T : class
    {

        public void Delete(object id)
        {
            T obj = GetById(id);
            BinaryFileUtil.RemoveFromFile(obj);
        }

        public IEnumerable<T> GetAll()
        {
            return BinaryFileUtil.ReadFromFile<T>().AsEnumerable<T>();
        }

        public T GetById(object id)
        {
            if (typeof(T).Equals(typeof(StudentModel)))
            {
                List<StudentModel> list = BinaryFileUtil.ReadFromFile<StudentModel>();
                return list.Find(x => x.Id == (int)id) as T;
            }
            else if (typeof(T).Equals(typeof(CourseModel)))
            {
                List<CourseModel> list = BinaryFileUtil.ReadFromFile<CourseModel>();
                return list.Find(x => x.Id == (int)id) as T;
            }
            else if (typeof(T).Equals(typeof(EmployeeModel)))
            {
                List<EmployeeModel> list = BinaryFileUtil.ReadFromFile<EmployeeModel>();
                return list.Find(x => x.Id == (int)id) as T;
            }
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            return obj;
        }

        public void Insert(T obj)
        {
            BinaryFileUtil.WriteToFile<T>(obj);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            int id = -1;
            if (typeof(T).Equals(typeof(StudentModel)))
            {
                StudentModel tmpObj = obj as StudentModel;
                id = tmpObj.Id;
            }
            else if (typeof(T).Equals(typeof(CourseModel)))
            {
                CourseModel tmpObj = obj as CourseModel;
                id = tmpObj.Id;
            }
            else if (typeof(T).Equals(typeof(EmployeeModel)))
            {
                EmployeeModel tmpObj = obj as EmployeeModel;
                id = tmpObj.Id;
            }
            if (id > -1)
            {
                Delete(id);
            }
            Insert(obj);
        }
        public void HandleLink(T obj, object reference, bool add)
        {
            if (typeof(T).Equals(typeof(CourseModel)) && reference is StudentModel)
            {
                //CourseModel currentCourse = (CourseModel)Convert.ChangeType(obj, typeof(CourseModel));
                CourseModel currentCourse = obj as CourseModel;
                StudentModel tmpStudent = (StudentModel)reference;
                currentCourse.AttendingStudents.Add(tmpStudent);
                Update(currentCourse as T);
                //Write to the studentfil as well
                IBaseRepository<StudentModel> baseRepositoryStudent = RepositoryFactory.GetRepository<StudentModel>();
                StudentModel student = baseRepositoryStudent.GetById(tmpStudent.Id);
                student.Courses.Add(currentCourse);
                baseRepositoryStudent.Update(student);
            }
        }
    }
}

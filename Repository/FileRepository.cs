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
            throw new NotImplementedException();
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
            else if(typeof(T).Equals(typeof(CourseModel)))
            {
                List<CourseModel> list = BinaryFileUtil.ReadFromFile<CourseModel>();
                return list.Find(x => x.Id == (int)id) as T;
            }
            else if (typeof(T).Equals(typeof(EmployeeModel)))
            {
                List<CourseModel> list = BinaryFileUtil.ReadFromFile<CourseModel>();
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
            Insert(obj);
        }
        public void HandleLink(T obj, object reference, bool add)
        {

        }

    }
}

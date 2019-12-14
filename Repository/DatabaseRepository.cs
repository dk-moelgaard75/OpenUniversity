using OpenUniversity.Data;
using OpenUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace OpenUniversity.Repository
{
    class DatabaseRepository<T> : IBaseRepository<T> where T : class
    {
        private OpenUniversityDbContext _context = null;
        private DbSet<T> table = null;
        public DatabaseRepository()
        {
            this._context = new OpenUniversityDbContext();
            table = _context.Set<T>();
        }
        public DatabaseRepository(OpenUniversityDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        
        public OpenUniversityDbContext Context
        {
            get { return _context; }
        }
        
        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> list = null;
            if(typeof(T).Equals(typeof(StudentModel)))
            {
                List<StudentModel> studentList = new List<StudentModel>();
                foreach(StudentModel student in _context.Students.Include("Courses"))
                {
                    studentList.Add(student);
                }
                return studentList.Cast<T>();
            }
            else if (typeof(T).Equals(typeof(CourseModel)))
            {
                List<CourseModel> courseList = new List<CourseModel>();
                foreach (CourseModel course in _context.Courses.Include(x => x.AttendingStudents))
                {
                    courseList.Add(course);
                }
                return courseList.Cast<T>();
            }
            else
            {
                list = table.ToList();
            }
            
            return list;
            
            //return table.ToList();

        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
            _context.SaveChanges();
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            _context.SaveChanges();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

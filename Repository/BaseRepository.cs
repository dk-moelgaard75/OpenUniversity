using OpenUniversity.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace OpenUniversity.Repository
{
    class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private OpenUniversityDbContext _context = null;
        private DbSet<T> table = null;
        public BaseRepository()
        {
            this._context = new OpenUniversityDbContext();
            table = _context.Set<T>();
        }
        public BaseRepository(OpenUniversityDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

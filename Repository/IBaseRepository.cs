using OpenUniversity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        //OpenUniversityDbContext Context { get; }
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(object id);
        void Save();
        void HandleLink(TEntity obj, object reference, bool add);
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfo.Data.Common
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        IQueryable<T> FindBySingle(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindByAuto(System.Linq.Expressions.Expression<Func<T, bool>> predicate);     

        T Add(T entity);

        T Delete(T entity);

        T Update(T entity, int key);

        T Update(T entity, string key);
        bool AddNew(T entity);
        void Save();
    }
}
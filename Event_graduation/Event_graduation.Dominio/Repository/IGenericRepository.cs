using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Event_graduation.Dominio.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> ListEvent();
        T GetEventById(int id);
        void AddEvent(T entity);
        void UpdateEvent(T entity, params Expression<Func<T, object>>[] propertiesToUpdate);
        void DeleteEvent(int id);
    }
}

using Event_graduation.Dominio.Repository;
using Event_graduation.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Event_graduation.Persistence
{
    public class GenericRepositoryImpl<T> : IGenericRepository<T> where T : class
    {

        private readonly EventContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepositoryImpl(EventContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public void AddEvent(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteEvent(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public T GetEventById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> ListEvent()
        {
           return  _dbSet.ToList();
        }
        public void UpdateEvent(T entity, params Expression<Func<T, object>>[] propertiesToUpdate)
        {
            _dbSet.Attach(entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;

            // Solo actualiza las propiedades especificadas
            foreach (var property in propertiesToUpdate)
            {
                entry.Property(property).IsModified = true;
            }

            _context.SaveChanges();
        }

        /*
        public void UpdateEvent(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }*/
    }
}

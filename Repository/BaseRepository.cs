using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OOAD.Data;

namespace OOAD.Repository
{
    public class BaseRepository<T> where T : class
    {
        protected readonly AppDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public IQueryable<T> Query => _dbSet.AsQueryable();

        public BaseRepository(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual T? GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public virtual bool Remove(Guid id)
        {
            var entity = GetById(id);
            if (entity == null)
                return false;
            _dbSet.Remove(entity);
            return true;
        }
        public virtual bool Remove(T entity)
        {
            if (entity == null)
                return false;
            _dbSet.Remove(entity);
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
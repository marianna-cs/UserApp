using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Repo
{
    public abstract class Repository<T> where T : class
    {
        UserAppContext _context;
        public Repository(UserAppContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAllList()
        {
            return _context.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            return _context.Find<T>(id);
        }

        public void Create(T entity)
        {
            _context.Add<T>(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Remove<T>(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

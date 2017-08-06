using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskAppBackEnd.Core
{
    public class UnitOfWork : IUnitOfWork
    {

        private DbContext _context;

        public DbContext DbContext

        { get { return _context; } }

        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Save();
        }

    }
}

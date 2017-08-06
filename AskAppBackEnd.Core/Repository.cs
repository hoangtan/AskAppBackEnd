using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AskAppBackEnd.Core
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dbset;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbContext _dbContext;
        public Repository(IUnitOfWork unitOfWork)
        {
            _dbset = unitOfWork.Set<T>();
            _unitOfWork = unitOfWork;
            _dbContext = unitOfWork.DbContext;
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> queryShaper, CancellationToken cancellationToken)
        {
            var query = queryShaper(GetAll());
            return await query.ToArrayAsync(cancellationToken);
        }

        public virtual async Task<TResult> GetAsync<TResult>(Func<IQueryable<T>, TResult> queryShaper, CancellationToken cancellationToken)
        {
            var set = GetAll();
            var query = queryShaper;
            var factory = Task<TResult>.Factory;
            return await factory.StartNew(() => query(set), cancellationToken);
        }

        public T GetById(Guid id)
        {
            return _dbset.Find(id);
        }


        public IQueryable<T> GetAll()
        {
            return _dbset;
        }

        public T Insert(T entity)
        {
            return _dbset.Add(entity);
        }


        public T Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }


        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }


        public void Save()
        {
            _unitOfWork.Save();
        }

        public async Task<int> SaveAsync()
        {
            return await _unitOfWork.SaveAsync();
        }
    }
}

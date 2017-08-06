using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AskAppBackEnd.Core
{
    public interface IRepository<T>
    {
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> queryShaper, CancellationToken cancellationToken);

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required for generics support.")]
        Task<TResult> GetAsync<TResult>(Func<IQueryable<T>, TResult> queryShaper, CancellationToken cancellationToken);

        T GetById(Guid id);

        IQueryable<T> GetAll();

        T Update(T entity);

        T Insert(T entity);

        void Delete(T entity);

        void Save();

        Task<int> SaveAsync();
    }
}
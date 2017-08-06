using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AskAppBackEnd.Core
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }
        DbSet<T> Set<T>() where T : class;
        void Save();
        Task<int> SaveAsync();
    }
}